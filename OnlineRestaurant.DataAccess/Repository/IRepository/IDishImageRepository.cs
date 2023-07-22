using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Repository.IRepository;

public interface IDishImageRepository : IRepository<DishImageModel>
{
    Task UpdateAsync(DishImageModel entity);
}