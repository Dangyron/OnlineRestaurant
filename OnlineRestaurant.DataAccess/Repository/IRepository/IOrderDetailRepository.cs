using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Repository.IRepository;

public interface IOrderDetailRepository : IRepository<OrderDetailModel>
{
    Task UpdateAsync(OrderDetailModel entity);
}