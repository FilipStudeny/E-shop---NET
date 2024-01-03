namespace Eshop.Shared.Models.Order;

public class OrderProductDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ProductType { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}