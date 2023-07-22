using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OnlineRestaurant.Models;

public class DishImageModel
{
    [Key]
    public int Id { get; set; }

    [Required] public byte[] Image { get; set; } = null!;
    
    public int DishId { get; set; }
    
    [ForeignKey(nameof(DishId))]
    public DishModel Dish { get; set; }
    
    [ValidateNever]
    public DateTime CreationDate { get; set; }
    
    [ValidateNever]
    public List<DateTime>? UpdateDates { get; set; }
}