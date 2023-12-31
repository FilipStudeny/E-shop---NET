namespace Eshop.Shared.Models.Cart;

public class CartItem
{
    public int ProductId { get; set; }
    public int ProductTypeId { get; set; }
    public int Quantity { get; set; } = 1;
}