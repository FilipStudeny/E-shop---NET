using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshop.Shared.Models.ProductModels;

namespace Eshop.Shared.Models.ProductModels;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public List<Image> Images { get; set; } = new();

    public List<ProductVariant> Variants { get; set; } = new (); //for ex. book - hard copy or ebook
    
    // Relationship with Category
    public Category? Category { get; set; }
    public int CategoryId { get; set; }
    public bool FeaturedProduct { get; set; } = false;
    
    public bool Visible { get; set; } = true;
    public bool Deleted { get; set; } = false;
    // for form edits, not visible in the DB
    [NotMapped]
    public bool Editing { get; set; } = false;
    [NotMapped]
    public bool IsNew { get; set; } = false;
}