using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineRestaurant.DataAccess.Repository.IRepository;
using OnlineRestaurant.Models;
using OnlineRestaurant.Models.ViewModels;
using OnlineRestaurant.Utility;

namespace OnlineRestaurant.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles=Constants.RoleAdmin)]
public class DishController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public DishController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = await _unitOfWork.Dishes.GetAllAsync(includedProperties:"Category");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();

        if (categories is null)
            throw new Exception("Categories mustn't be null");
        
        var dishVm = new DishViewModel
        {
            Categories = categories.Select(i => new SelectListItem
            {
                Text = i.Title,
                Value = i.Id.ToString()
            }),
            Dish = new ()
        };
        return View(dishVm);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(DishViewModel model, List<IFormFile>? files)
    {
        await NameValidation(model.Dish);
        
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.Dish.CreationDate = DateTime.UtcNow;

        await _unitOfWork.Dishes.AddAsync(model.Dish);
        
        await _unitOfWork.SaveChangesAsync();

        if (files is not null)
            await WriteImagesToDbAsync(files, model.Dish.Id);
        
        await _unitOfWork.SaveChangesAsync();
        
        TempData["success"] = "Dish created successfully";
        return RedirectToAction("Index");
    }

    private async Task WriteImagesToDbAsync(List<IFormFile> files, int id)
    {
        foreach (var file in files)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var imageInBytes = memoryStream.ToArray();
            await _unitOfWork.DishImages.AddAsync(new DishImageModel
            {
                CreationDate = DateTime.UtcNow,
                Image = imageInBytes,
                DishId = id,
            });
        }
    }

    private async Task NameValidation(DishModel model, int id = 0)
    {
        var name = model.Name.ToLower();
        if (id != 0)
        {
            var dish = await _unitOfWork.Dishes.GetByIdAsync(id);
            if (dish is not null && dish.Name == model.Name)
                return;
        }
        
        var dishes = await _unitOfWork.Dishes.GetAllAsync();
        
        if (dishes is not null)
        {
            foreach (var dish in dishes)
            {
                if (name == dish.Name.ToLower())
                {
                    ModelState.AddModelError(nameof(DishModel.Name), "This name already exists!");
                    return;
                }
            }
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null or 0)
            return NotFound();
        
        var categories = await _unitOfWork.Categories.GetAllAsync();

        if (categories is null)
            throw new Exception($"Categories mustn't be null");
        
        var dishVm = new DishViewModel
        {
            Categories = categories.Select(i => new SelectListItem
            {
                Text = i.Title,
                Value = i.Id.ToString()
            }),
            Dish = (await _unitOfWork.Dishes.GetByIdAsync(id.Value))!
        };
        
        return View(dishVm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DishViewModel model)
    {
        await NameValidation(model.Dish, model.Dish.Id);

        if (!ModelState.IsValid)
            return View(model);
        
        model.Dish.CreationDate = model.Dish.CreationDate.ToUniversalTime();
        await _unitOfWork.Dishes.UpdateAsync(model.Dish);
        await _unitOfWork.SaveChangesAsync();

        TempData["success"] = "Category updated successfully";

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null or 0)
            return NotFound();

        var dish = await _unitOfWork.Dishes.GetByIdAsync(id.Value);

        if (dish == null)
            return NotFound();

        return View(dish);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var dish = await _unitOfWork.Categories.GetAsync(x => x.Id == id);
        if (dish == null)
            return NotFound();

        await _unitOfWork.Categories.DeleteAsync(dish);
        await _unitOfWork.SaveChangesAsync();
        
        TempData["success"] = "Category deleted successfully";
        
        return RedirectToAction("Index");
    }
}