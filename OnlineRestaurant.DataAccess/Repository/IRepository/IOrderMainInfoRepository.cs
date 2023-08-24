using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Repository.IRepository;

public interface IOrderMainInfoRepository : IRepository<OrderMainInfoModel>
{
    Task UpdateAsync(OrderMainInfoModel entity);
}