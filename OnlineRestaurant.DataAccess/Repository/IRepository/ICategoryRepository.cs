using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Repository.IRepository;

public interface ICategoryRepository : IRepository<CategoryModel>
{
    Task UpdateAsync(CategoryModel entity);
}