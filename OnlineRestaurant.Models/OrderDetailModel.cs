using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OnlineRestaurant.Models.Validators;

namespace OnlineRestaurant.Models;

public class OrderDetailModel
{
    [Key]
    public int Id { get; set; }
    
    [CountValidation]
    public int Count { get; set; }
    
    [NotMapped]
    public float Price => Dish.Price * Count;
    
    public int DishId { get; set; }

    [ForeignKey(nameof(DishId))]
    [ValidateNever]
    public DishModel Dish { get; set; } = null!;
    
    public int OrderMainInfoId { get; set; }

    [ForeignKey(nameof(OrderMainInfoId))]
    [ValidateNever]
    public OrderMainInfoModel OrderMainInfo { get; set; } = null!;
}