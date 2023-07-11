using Microsoft.EntityFrameworkCore;

namespace OnlineRestaurant.DataAccess.Data;

public class OnlineRestaurantDbContext : DbContext
{
    public OnlineRestaurantDbContext(DbContextOptions<OnlineRestaurantDbContext> options) : base(options) {}
    
    
}