using Microsoft.AspNetCore.Mvc;
using OnlineRestaurant.DataAccess.Repository.IRepository;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.Web.Controllers;

public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _unitOfWork.Categories.GetAllAsync());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CategoryModel model)
    {
        await NameValidation(model);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.CreationDate = DateTime.UtcNow;

        await _unitOfWork.Categories.AddAsync(model);
        await _unitOfWork.SaveChangesAsync();
        
        TempData["success"] = "Category created successfully";
        return RedirectToAction("Index");
    }

    private async Task NameValidation(CategoryModel model, int id = 0)
    {
        var name = model.Title.ToLower();
        if (id != 0)
        {
            var dish = await _unitOfWork.Categories.GetByIdAsync(id);
            if (dish is not null && dish.Title == model.Title)
                return;
        }
        var categories = await _unitOfWork.Categories.GetAllAsync();
        
        if (categories is not null)
        {
            foreach (var category in categories)
            {
                if (name == category.Title.ToLower())
                {
                    ModelState.AddModelError(nameof(CategoryModel.Title), "This name already exists!");
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

        var category = await _unitOfWork.Categories.GetByIdAsync(id.Value);

        if (category == null)
            return NotFound();

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryModel model)
    {
        await NameValidation(model);

        if (!ModelState.IsValid)
            return View();
        
        model.CreationDate = model.CreationDate.ToUniversalTime();
        await _unitOfWork.Categories.UpdateAsync(model);
        await _unitOfWork.SaveChangesAsync();

        TempData["success"] = "Category updated successfully";

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null or 0)
            return NotFound();

        var category = await _unitOfWork.Categories.GetByIdAsync(id.Value);

        if (category == null)
            return NotFound();

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id.Value);
        if (category == null)
            return NotFound();

        await _unitOfWork.Categories.DeleteAsync(category);
        await _unitOfWork.SaveChangesAsync();
        
        TempData["success"] = "Category deleted successfully";
        
        return RedirectToAction("Index");
    }
}