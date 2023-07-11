using System.ComponentModel.DataAnnotations;
using OnlineRestaurant.Models.Validators;

namespace OnlineRestaurant.Models;

public class CategoryModel
{
    [Key]
    public int Id { get; set; }
    
    [TitleValidation]
    public string Title { get; set; } = null!;
    
    public DateTime CreationDate { get; set; }
    
    public List<DateTime>? UpdateDates { get; set; }
}