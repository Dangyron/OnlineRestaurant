using OnlineRestaurant.DataAccess.Data;
using OnlineRestaurant.DataAccess.Repository.IRepository;

namespace OnlineRestaurant.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly OnlineRestaurantDbContext _dbContext;
    public ICategoryRepository Categories { get; }
    public IDishRepository Dishes { get;}
    public IDishImageRepository DishImages { get; }

    public UnitOfWork(OnlineRestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
        Categories = new CategoryRepository(dbContext);
        Dishes = new DishRepository(dbContext);
        DishImages = new DishImageRepository(dbContext);
    }

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}