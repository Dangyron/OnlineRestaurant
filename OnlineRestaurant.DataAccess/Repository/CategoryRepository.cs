using OnlineRestaurant.DataAccess.Data;
using OnlineRestaurant.DataAccess.Repository.IRepository;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Repository;

public class CategoryRepository : Repository<CategoryModel>, ICategoryRepository
{
    private readonly OnlineRestaurantDbContext _dbContext;

    public CategoryRepository(OnlineRestaurantDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateAsync(CategoryModel entity)
    {
        var model = await GetAsync(i => i.Id == entity.Id);

        if (model is null)
            throw new ArgumentException($"{nameof(entity)} with id = {entity.Id} there isn't in the Database");
        
        model.UpdateDates ??= new();
        
        model.UpdateDates.Add(DateTime.UtcNow);
        model.Title = entity.Title;

        await _dbContext.SaveChangesAsync();
    }
}