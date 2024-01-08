using Eshop.Shared.Models.Books;

namespace Eshop.Client.Services.BookTypeService;

public interface IBookTypeService
{
    event Action OnChange;
    public List<BookType>? BookTypes { get; set; }
    
    Task GetBookTypes();
    Task AddBookType(BookType bookType);
    Task UpdateBookType(BookType bookType);
    BookType CreateNewBookType();
}