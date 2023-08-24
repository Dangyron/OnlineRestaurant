using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Repository.IRepository;

public interface IUserRepository : IRepository<UserModel>
{
    Task UpdateAsync(UserModel model);
}