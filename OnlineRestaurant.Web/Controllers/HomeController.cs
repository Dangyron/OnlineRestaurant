using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineRestaurant.DataAccess.Repository.IRepository;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.Web.Controllers;

public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public HomeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        var dishes = await _unitOfWork.Dishes.GetAllAsync(includedProperties: "Category,Images");
        return View(dishes);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Details(int? id)
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}