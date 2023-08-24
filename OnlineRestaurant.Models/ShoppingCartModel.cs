using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OnlineRestaurant.Models.Validators;

namespace OnlineRestaurant.Models;

public class ShoppingCartModel
{
    [Key]
    public int Id { get; set; }
    
    [CountValidation]
    public int Count { get; set; }
    
    [NotMapped]
    public float Price { get; set; }
    
    public int DishId { get; set; }

    [ForeignKey(nameof(DishId))]
    [ValidateNever]
    public DishModel Dish { get; set; } = null!;

    public string UserId { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    [ValidateNever]
    public UserModel User { get; set; } = null!;
}