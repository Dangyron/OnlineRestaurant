using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.DataAccess.Repository.IRepository;

namespace OnlineRestaurant.DataAccess.Repository;

public abstract class Repository<T> : IRepository<T> where T: class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    protected Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public async Task<List<T>?> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includedProperties = null, bool isNeedToTrack = false)
    {
        IQueryable<T> query = _dbSet;

        if (!isNeedToTrack)
            query = query.AsNoTracking();
        
        if (filter is not null)
            query = query.Where(filter);
        
        if (!string.IsNullOrEmpty(includedProperties))
        {
            foreach (var props in includedProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(props);
            }
        }
        
        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includedProperties = null, bool isNeedToTrack = false)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (filter is null)
            return null;
        
        IQueryable<T> query = _dbSet;
        
        if (!isNeedToTrack)
            query = query.AsNoTracking();
        
        query = query.Where(filter);

        if (string.IsNullOrEmpty(includedProperties)) 
            return await query.FirstOrDefaultAsync();
        
        foreach (var props in includedProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(props);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}