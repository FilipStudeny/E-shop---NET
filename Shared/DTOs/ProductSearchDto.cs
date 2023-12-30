using Eshop.Shared.Models;

namespace Eshop.Shared.DTOs;

public class ProductSearchDto
{
    public List<Product> Products { get; set; } = new();
    public int Pages { get; set; }
    public int CurrentPage { get; set; }
}