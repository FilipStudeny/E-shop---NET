namespace Eshop.Shared.Models.Order;

public class OrderDetailDto
{
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderProductDetailDto> Products { get; set; }
}