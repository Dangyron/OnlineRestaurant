using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Data;

public class OnlineRestaurantDbContext : DbContext
{
    public OnlineRestaurantDbContext(DbContextOptions<OnlineRestaurantDbContext> options) : base(options) {}
    
    public DbSet<CategoryModel> Categories { get; set; }
    
    public DbSet<DishModel> Dishes { get; set; }
    
    public DbSet<DishImageModel> DishImages { get; set; }

    public DbSet<UserModel> Users { get; set; }
    
    public DbSet<ShoppingCartModel> ShoppingCarts { get; set; }
    
    public DbSet<OrderDetailModel> OrderDetails { get; set; }

    public DbSet<OrderMainInfoModel> OrderMainInfos { get; set; }
}