using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.DataAccess.Data;
using OnlineRestaurant.DataAccess.Repository.IRepository;
using OnlineRestaurant.Models;
using OnlineRestaurant.Models.ViewModels;

namespace OnlineRestaurant.Web.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly OnlineRestaurantDbContext _dbContext;
    private readonly OnlineRestaurantIdentityDbContext _identityDbContext;

    public HomeController(IUnitOfWork unitOfWork, OnlineRestaurantDbContext dbContext, OnlineRestaurantIdentityDbContext identityDbContext)
    {
        _unitOfWork = unitOfWork;
        _dbContext = dbContext;
        _identityDbContext = identityDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        await CopyUsers();
        var dishes = await _unitOfWork.Dishes.GetAllAsync(includedProperties: "Category,Images");
        return View(dishes);
    }

    private async Task CopyUsers()
    {
        foreach (var user in _identityDbContext.AppUsers.AsNoTracking())
        {
            var anUser = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(i => i.Id == user.Id);

            if (anUser is not null)
                _dbContext.Users.Update(user);
            else
                await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();
        }
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null or 0)
            return NotFound();
        
        var dish = await _unitOfWork.Dishes.GetAsync(i => i.Id == id, includedProperties: "Category,Images");
        
        if (dish is null)
            return NotFound();

        var shoppingCart = new ShoppingCartModel
        {
            Dish = dish,
            DishId = id.Value,
            Count = 1,
        };
        
        return View(shoppingCart);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Details(ShoppingCartModel model)
    {
        var userIdentity = (ClaimsIdentity)User.Identity!;

        var userId = userIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        model.UserId = userId;
        
        var cart = await _unitOfWork.ShoppingCarts.GetAsync(i => i.UserId == userId && i.DishId == model.DishId);

        if (cart is null)
            await _unitOfWork.ShoppingCarts.AddAsync(model);
        else
        {
            cart.Count += model.Count;
            await _unitOfWork.ShoppingCarts.UpdateAsync(cart);
        }
        
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}