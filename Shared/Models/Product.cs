using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Shared.Models;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    public List<ProductVariant> Variants { get; set; } = new (); //for ex. book - hard copy or ebook
    
    // Relationship with Category
    public Category? Category { get; set; }
    public int CategoryId { get; set; }

    public bool FeaturedProduct { get; set; } = false;
}