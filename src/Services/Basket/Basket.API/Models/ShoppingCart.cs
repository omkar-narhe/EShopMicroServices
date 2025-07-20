namespace Basket.API.Models;

public class ShoppingCart
{
    public string Username { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public double TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public ShoppingCart(string userName)
    {
        Username = userName;
    }

    //Required for mapping
    public ShoppingCart()
    {
        
    }
}