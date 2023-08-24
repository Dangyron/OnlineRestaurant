using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.DataAccess.Data;
using OnlineRestaurant.DataAccess.Repository.IRepository;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Repository;

public class ShoppingCartRepository : Repository<ShoppingCartModel>, IShoppingCartRepository
{
    private readonly OnlineRestaurantDbContext _dbContext;

    public ShoppingCartRepository(OnlineRestaurantDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateAsync(ShoppingCartModel entity)
    {
        var model = await GetByIdAsync(entity.Id);
        
        if (model is null)
            throw new ArgumentException($"{nameof(entity)} with id = {entity.Id} there isn't in the Database");

        model.Count = entity.Count;
        model.DishId = entity.DishId;
        model.UserId = entity.UserId;
        
        await _dbContext.SaveChangesAsync();
    }
}