﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OnlineRestaurant.Models.Validators;

namespace OnlineRestaurant.Models;

public class CategoryModel
{
    [Key]
    public int Id { get; set; }
    
    [TitleValidation]
    public string Title { get; set; } = null!;
    
    [ValidateNever]
    public DateTime CreationDate { get; set; }
    
    [ValidateNever]
    public List<DateTime>? UpdateDates { get; set; }
}