using Microsoft.EntityFrameworkCore;

namespace OnlineRestaurant.DataAccess.Data;

public class OnlineRestaurantDbConnection : DbContext
{
    public OnlineRestaurant(DbContextOptions<On>)
}