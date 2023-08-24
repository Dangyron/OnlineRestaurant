namespace OnlineRestaurant.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository Categories { get; }
    IDishRepository Dishes { get; }
    IDishImageRepository DishImages { get; }
    IUserRepository Users { get; }
    IShoppingCartRepository ShoppingCarts { get; }
    IOrderMainInfoRepository OrderMainInfos { get; }
    IOrderDetailRepository OrderDetails { get; }
    Task SaveChangesAsync();
}