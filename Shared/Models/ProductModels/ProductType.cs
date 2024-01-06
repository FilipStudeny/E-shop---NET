
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Shared.Models.ProductModels;

public class ProductType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
        
    // for form edits, not visible in the DB
    [NotMapped]
    public bool Editing { get; set; } = false;
    [NotMapped]
    public bool IsNew { get; set; } = false;
}