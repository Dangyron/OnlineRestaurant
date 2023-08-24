using OnlineRestaurant.DataAccess.Data;
using OnlineRestaurant.DataAccess.Repository.IRepository;

namespace OnlineRestaurant.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly OnlineRestaurantDbContext _dbContext;
    private readonly OnlineRestaurantIdentityDbContext _identityDbContext;
    public ICategoryRepository Categories { get; }
    public IDishRepository Dishes { get;}
    public IDishImageRepository DishImages { get; }
    public IUserRepository Users { get; }
    public IShoppingCartRepository ShoppingCarts { get; }
    public IOrderMainInfoRepository OrderMainInfos { get; }
    public IOrderDetailRepository OrderDetails { get; }

    public UnitOfWork(OnlineRestaurantDbContext dbContext, OnlineRestaurantIdentityDbContext identityDbContext)
    {
        _dbContext = dbContext;
        _identityDbContext = identityDbContext;
        Categories = new CategoryRepository(dbContext);
        Dishes = new DishRepository(dbContext);
        DishImages = new DishImageRepository(dbContext);
        Users = new UserRepository(identityDbContext);
        ShoppingCarts = new ShoppingCartRepository(dbContext);
        OrderMainInfos = new OrderMainInfoRepository(dbContext);
        OrderDetails = new OrderDetailRepository(dbContext);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
        await _identityDbContext.SaveChangesAsync();
    }
}