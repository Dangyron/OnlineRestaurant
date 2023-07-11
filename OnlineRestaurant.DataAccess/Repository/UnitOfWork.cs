using OnlineRestaurant.DataAccess.Data;
using OnlineRestaurant.DataAccess.Repository.IRepository;

namespace OnlineRestaurant.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly OnlineRestaurantDbContext _dbContext;
    public ICategoryRepository Categories { get; }
    
    public UnitOfWork(OnlineRestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
        Categories = new CategoryRepository(dbContext);
    }

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}