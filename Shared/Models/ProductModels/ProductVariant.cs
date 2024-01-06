using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Eshop.Shared.Models.ProductModels;

public class ProductVariant
{
    //Composite-primary key
    [JsonIgnore] //Fix for circular reference
    public Product Product { get; set; }
    public int ProductId { get; set; }
    
    public ProductType ProductType { get; set; }
    public int ProductTypeId { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal OriginalPrice { get; set; } //For sales
    
    
    public bool Visible { get; set; } = true;
    public bool Deleted { get; set; } = false;
    // for form edits, not visible in the DB
    [NotMapped]
    public bool Editing { get; set; } = false;
    [NotMapped]
    public bool IsNew { get; set; } = false;
}