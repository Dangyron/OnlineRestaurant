using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.DataAccess.Repository.IRepository;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.DataAccess.Repository;

public class OrderMainInfoRepository : Repository<OrderMainInfoModel>, IOrderMainInfoRepository
{
    private readonly DbContext _dbContext;

    public OrderMainInfoRepository(DbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateAsync(OrderMainInfoModel entity)
    {
        var model = await GetAsync(i => i.Id == entity.Id);

        if (model is null)
            throw new ArgumentException($"{nameof(entity)} with id = {entity.Id} there isn't in the Database");

        model.UserId = entity.UserId;
        model.OrderDate = entity.OrderDate;
        model.ShippingDate = entity.ShippingDate;
        model.PaymentDate = entity.PaymentDate;
        model.OrderStatus = entity.OrderStatus;
        model.OrderTotal = entity.OrderTotal;
        model.PaymentStatus = entity.PaymentStatus;
        model.PaymentDueDate = model.PaymentDueDate;

        await _dbContext.SaveChangesAsync();
    }
}