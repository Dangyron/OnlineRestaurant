using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Data;

public class OnlineRestaurantIdentityDbContext : IdentityDbContext
{
    public OnlineRestaurantIdentityDbContext(DbContextOptions<OnlineRestaurantIdentityDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<UserModel> AppUsers { get; set; }
}