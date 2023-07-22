using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OnlineRestaurant.Models.Validators;

namespace OnlineRestaurant.Models;

public class DishModel
{
    [Key]
    public int Id { get; set; }
    
    [TitleValidation]
    public string Name { get; set; } = null!;
    
    [Required]
    public string Description { get; set; } = null!;
    
    [Required]
    public float Price { get; set; }
    
    public int VisitCount { get; set; }
    
    public int CategoryId { get; set; }
    
    [ForeignKey(nameof(CategoryId))]
    [ValidateNever]
    public CategoryModel Category { get; set; }
    
    [ValidateNever]
    public List<DishImageModel>? Images { get; set; }
    
    [ValidateNever]
    public DateTime CreationDate { get; set; }
    
    [ValidateNever]
    public List<DateTime>? UpdateDates { get; set; }
}