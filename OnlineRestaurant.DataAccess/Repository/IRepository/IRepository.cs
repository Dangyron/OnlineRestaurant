using System.Linq.Expressions;

namespace OnlineRestaurant.DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    Task<List<T>?> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    Task<T?> GetAsync(Expression<Func<T, bool>> filter);
    Task AddAsync(T entity);
    Task DeleteAsync(T entity);
}