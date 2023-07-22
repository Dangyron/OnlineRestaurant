namespace OnlineRestaurant.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository Categories { get; }
    IDishRepository Dishes { get; }
    IDishImageRepository DishImages { get; }
    Task SaveChangesAsync();
}