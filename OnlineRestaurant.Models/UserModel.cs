using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace OnlineRestaurant.Models;

public class UserModel : IdentityUser
{
    [Required] public string Name { get; set; } = null!;

    [Required] public string Address { get; set; } = null!;

    public string Role { get; set; } = null!;
}