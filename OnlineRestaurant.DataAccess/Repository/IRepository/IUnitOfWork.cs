namespace OnlineRestaurant.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository Categories { get; }
    Task SaveChangesAsync();
}