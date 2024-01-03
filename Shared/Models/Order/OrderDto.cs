namespace Eshop.Shared.Models.Order;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Product { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}