using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OnlineRestaurant.Models;

public class OrderMainInfoModel
{
    [Key]
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }
    public DateTime ShippingDate { get; set; }
    public float OrderTotal { get; set; }
    
    public string? OrderStatus { get; set; }
    public string? PaymentStatus { get; set; }
    
    public DateTime PaymentDate { get; set; }
    public DateTime PaymentDueDate { get; set; }
    
    public string UserId { get; set; } = string.Empty;

    [ValidateNever]
    [ForeignKey(nameof(UserId))]
    public UserModel User { get; set; } = null!;
}