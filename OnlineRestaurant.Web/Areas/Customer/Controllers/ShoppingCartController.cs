using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineRestaurant.DataAccess.Repository.IRepository;
using OnlineRestaurant.Models;
using OnlineRestaurant.Models.ViewModels;
using OnlineRestaurant.Utility;

namespace OnlineRestaurant.Web.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize]
public class ShoppingCartController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ShoppingCartController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var shoppingCartVm = await GetShoppingCartVm();

        return View(shoppingCartVm);
    }
    
    [HttpGet]
    public async Task<IActionResult> PurchasePreparation()
    {
        var shoppingCartVm = await GetShoppingCartVm();
        
        return View(shoppingCartVm);
    }

    [HttpPost]
    public async Task<IActionResult> Purchase()
    {
        var shoppingCartVm = await GetShoppingCartVm(false);
        
        shoppingCartVm.OrderMainInfo!.OrderDate = DateTime.UtcNow;

        shoppingCartVm.OrderMainInfo!.OrderStatus = Constants.StatusPending;
        shoppingCartVm.OrderMainInfo!.PaymentStatus = Constants.PaymentStatusPending;

        await _unitOfWork.OrderMainInfos.AddAsync(shoppingCartVm.OrderMainInfo);
        await _unitOfWork.SaveChangesAsync();

        foreach (var shoppingCart in shoppingCartVm.ShoppingCarts!)
        {
            var orderDetail = new OrderDetailModel
            {
                DishId = shoppingCart.DishId,
                OrderMainInfoId = shoppingCartVm.OrderMainInfo.Id,
                Count = shoppingCart.Count,
            };

            await _unitOfWork.OrderDetails.AddAsync(orderDetail);
            await _unitOfWork.SaveChangesAsync();
        }

        TempData["success"] = "Your order has been confirmed";
        
        return RedirectToAction(nameof(OrderConfirmed), new {shoppingCartVm.OrderMainInfo.Id});
    }

    public async Task<IActionResult> OrderConfirmed(int id)
    {
        var shoppingCart = (await GetShoppingCartVm()).ShoppingCarts?.ToList();

        if (shoppingCart is not null)
        {
            foreach (var data in shoppingCart)
            {
                await _unitOfWork.ShoppingCarts.DeleteAsync(data);
            }
            await _unitOfWork.SaveChangesAsync();
        }
        
        return View(id);
    }
    
    private async Task<ShoppingCartViewModel> GetShoppingCartVm(bool isNeedAnUser = true)
    {
        var userIdentity = (ClaimsIdentity)User.Identity!;
        var userId = userIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var shoppingCartVm = new ShoppingCartViewModel
        {
            ShoppingCarts =
                await _unitOfWork.ShoppingCarts.GetAllAsync(i => i.UserId == userId, includedProperties: "Dish"),
            OrderMainInfo = new()
            {
                UserId = userId,
                User = isNeedAnUser ? (await _unitOfWork.Users.GetAsync(i => i.Id == userId, isNeedToTrack:false))! : null,
            },
        };
        
        if (shoppingCartVm.ShoppingCarts is not null)
            ComputePriceForDishes(shoppingCartVm);
        
        return shoppingCartVm;
    }

    [HttpGet]
    public async Task<IActionResult> AddOneDish(int cartId)
    {
        var cart = await _unitOfWork.ShoppingCarts.GetAsync(i => i.Id == cartId);
        if (cart is null)
            return NotFound();

        cart.Count++;
        await _unitOfWork.ShoppingCarts.UpdateAsync(cart);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> RemoveOneDish(int cartId)
    {
        var cart = await _unitOfWork.ShoppingCarts.GetAsync(i => i.Id == cartId);
        if (cart is null)
            return NotFound();

        if (cart.Count == 1)
            await _unitOfWork.ShoppingCarts.DeleteAsync(cart);
        else
        {
            cart.Count--;
            await _unitOfWork.ShoppingCarts.UpdateAsync(cart);
        }

        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int cartId)
    {
        var cart = await _unitOfWork.ShoppingCarts.GetAsync(i => i.Id == cartId);
        if (cart is null)
            return NotFound();

        await _unitOfWork.ShoppingCarts.DeleteAsync(cart);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    
    private void ComputePriceForDishes(ShoppingCartViewModel shoppingCartVm)
    {
        foreach (var shoppingCart in shoppingCartVm.ShoppingCarts!)
        {
            shoppingCart.Price = shoppingCart.Dish.Price * shoppingCart.Count;
            shoppingCartVm.OrderMainInfo!.OrderTotal += shoppingCart.Price;
        }
    }
}