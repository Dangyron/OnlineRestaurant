using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Repository.IRepository;

public interface IDishRepository : IRepository<DishModel>
{
    Task UpdateAsync(DishModel entity);
}