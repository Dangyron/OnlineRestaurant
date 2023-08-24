namespace OnlineRestaurant.Models.ViewModels;

public class ShoppingCartViewModel
{
    public IEnumerable<ShoppingCartModel>? ShoppingCarts { get; set; }
    public OrderMainInfoModel? OrderMainInfo { get; set; }
}