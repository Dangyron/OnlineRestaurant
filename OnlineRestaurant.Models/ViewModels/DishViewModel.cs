using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineRestaurant.Models.ViewModels;

public class DishViewModel
{
    public DishModel Dish { get; set; }
    
    [ValidateNever]
    public IEnumerable<SelectListItem> Categories { get; set; }
}