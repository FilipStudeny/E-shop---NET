using Eshop.Shared.Models.Books;

namespace Eshop.Shared.DTOs;

public class BookSearchDto
{
    public List<Book> Books { get; set; } = new();
    public int Pages { get; set; }
    public int CurrentPage { get; set; }
}