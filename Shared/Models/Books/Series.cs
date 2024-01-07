using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Shared.Models.Books;

public class Series
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    
    [NotMapped]
    public bool Editing { get; set; } = false;
    [NotMapped]
    public bool IsNew { get; set; } = false;
    
}