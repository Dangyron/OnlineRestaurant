using System.Linq.Expressions;

namespace OnlineRestaurant.DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    Task<List<T>?> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includedProperties = null);
    Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includedProperties = null);
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task DeleteAsync(T entity);
}