using OnlineRestaurant.DataAccess.Data;
using OnlineRestaurant.DataAccess.Repository.IRepository;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Repository;

public class DishImageRepository : Repository<DishImageModel>, IDishImageRepository
{
    private readonly OnlineRestaurantDbContext _dbContext;

    public DishImageRepository(OnlineRestaurantDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateAsync(DishImageModel entity)
    {
        var model = await GetByIdAsync(entity.Id);

        if (model is null)
            throw new Exception("Dishes must contain image");

        model.UpdateDates ??= new();
        model.UpdateDates.Add(DateTime.UtcNow);
        model.Image = entity.Image;
        model.DishId = entity.DishId;

        await _dbContext.SaveChangesAsync();
    }
}