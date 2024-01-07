using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Shared.Models.Books;

public class BookType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    [NotMapped]
    public bool Editing { get; set; } = false;
    [NotMapped]
    public bool IsNew { get; set; } = false;
}