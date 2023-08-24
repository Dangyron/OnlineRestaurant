using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.DataAccess.Data;
using OnlineRestaurant.DataAccess.Repository.IRepository;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Repository;

public class UserRepository : Repository<UserModel>, IUserRepository
{
    private readonly DbContext _dbContext;

    public UserRepository(DbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateAsync(UserModel entity)
    {
        var model = await GetAsync(i => i.Id == entity.Id);
        
        if (model is null)
            throw new ArgumentException($"{nameof(entity)} with id = {entity.Id} there isn't in the Database");

        model.Name = entity.Name;
        model.Address = entity.Address;
        model.Role = entity.Role;
        model.Email = entity.Email;
        
        await _dbContext.SaveChangesAsync();
    }
}