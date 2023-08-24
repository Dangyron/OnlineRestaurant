using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Repository.IRepository;

public interface IShoppingCartRepository : IRepository<ShoppingCartModel>
{
    Task UpdateAsync(ShoppingCartModel entity);
}