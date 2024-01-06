using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Shared.Models.ProductModels;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    
    public bool Visible { get; set; } = true;
    public bool Deleted { get; set; } = false;
    
    // for form edits, not visible in the DB
    [NotMapped]
    public bool Editing { get; set; } = false;
    [NotMapped]
    public bool IsNew { get; set; } = false;
}