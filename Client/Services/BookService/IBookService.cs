using Eshop.Shared.DTOs.Books;
using Eshop.Shared.Models.Books;

namespace Eshop.Client.Services.BookService;

public interface IBookService
{
    event Action OnChange;
    string Message { get; set; }
    int CurrentPage { get; set; }
    int PageCount { get; set; }
    string LastSearch { get; set; }
    
    List<Book> Books { get; set; }
    List<FeaturedBookDto> FeaturedBooks { get; set; }
    List<Book> AdminBooks { get; set; }

    Task GetBooksByCategory(string category);
    Task GetFeaturedBooks();
    Task<ServiceResponse<Book>?> GetBook(int id);
    Task SearchForBooks(string search, int page);
    Task<List<string>> GetBookSearchSuggestion(string search);
    Task GetAdminBooks();

    Task<Book?> CreatBooks(Book product);
    Task<Book?> UpdateBook(Book product);
    Task DeleteBook(Book product);
}