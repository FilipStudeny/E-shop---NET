namespace Eshop.Shared.Models.Cart;

public class CartItem
{
    public int UserId { get; set; } //one user one cart
    public int ProductId { get; set; }
    public int ProductTypeId { get; set; }
    public int Quantity { get; set; } = 1;
}