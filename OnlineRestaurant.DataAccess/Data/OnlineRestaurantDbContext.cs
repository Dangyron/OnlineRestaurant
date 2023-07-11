using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Data;

public class OnlineRestaurantDbContext : DbContext
{
    public OnlineRestaurantDbContext(DbContextOptions<OnlineRestaurantDbContext> options) : base(options) {}
    
    public DbSet<CategoryModel> Categories { get; set; }
}