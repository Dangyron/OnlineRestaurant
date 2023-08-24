using OnlineRestaurant.DataAccess.Data;
using OnlineRestaurant.DataAccess.Repository.IRepository;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Repository;

public class DishRepository : Repository<DishModel>, IDishRepository
{
    private readonly OnlineRestaurantDbContext _dbContext;

    public DishRepository(OnlineRestaurantDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateAsync(DishModel entity)
    {
        var model = await GetAsync(i => i.Id == entity.Id);

        if (model is null)
            throw new ArgumentException($"{nameof(entity)} with id = {entity.Id} there isn't in the Database");
        
        model.UpdateDates ??= new();
        
        model.UpdateDates.Add(DateTime.UtcNow);
        model.Name = entity.Name;
        model.Description = entity.Description;
        model.Price = entity.Price;
        model.VisitCount = entity.VisitCount;
        model.CategoryId = entity.CategoryId;
        model.Images = entity.Images;

        await _dbContext.SaveChangesAsync();
    }
}