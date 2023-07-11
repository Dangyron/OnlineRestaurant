using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.DataAccess.Data;
using OnlineRestaurant.DataAccess.Repository.IRepository;

namespace OnlineRestaurant.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T: class
{
    private readonly OnlineRestaurantDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public Repository(OnlineRestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public async Task<List<T>?> GetAllAsync(Expression<Func<T, bool>>? filter = null)
    {
        if (filter is null)
            return await _dbSet.AsQueryable().AsNoTracking().ToListAsync();
        
        IQueryable<T> query = _dbSet;

        query = query.Where(filter);

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> filter)
    {
        if (filter is null)
            return null;
        
        IQueryable<T> query = _dbSet;
        
        query = query.Where(filter);

        return await query.FirstOrDefaultAsync();
    }

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}