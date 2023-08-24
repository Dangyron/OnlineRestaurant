using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.DataAccess.Repository.IRepository;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Repository;

public class OrderDetailRepository : Repository<OrderDetailModel>, IOrderDetailRepository
{
    private readonly DbContext _dbContext;

    public OrderDetailRepository(DbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateAsync(OrderDetailModel entity)
    {
        var model = await GetAsync(i => i.Id == entity.Id);

        if (model is null)
            throw new ArgumentException($"{nameof(entity)} with id = {entity.Id} there isn't in the Database");

        model.Count = entity.Count;
        model.DishId = entity.DishId;
        model.OrderMainInfoId = entity.OrderMainInfoId;

        await _dbContext.SaveChangesAsync();
    }
}