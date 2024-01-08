using Eshop.Shared.DTOs;
using Eshop.Shared.DTOs.Books;
using Eshop.Shared.Models.Books;

namespace Eshop.Server.Services.BookService;

public interface IBookService
{
    Task<ServiceResponse<List<Book>>> GetBooks();
    Task<ServiceResponse<Book>> GetBook(int id);
    
    Task<ServiceResponse<List<Book>>> GetBooksByCategory(string category);
    Task<ServiceResponse<BookSearchDto>> SearchForBooks(string search, int page);
    Task<ServiceResponse<List<string>>> GetBooksSearchSuggestions(string search);
    Task<ServiceResponse<List<FeaturedBookDto>>> GetFeaturedBooks();
    Task<ServiceResponse<List<Book>>> GetAdminBooks();
    Task<ServiceResponse<Book>> CreateBook(Book product);
    Task<ServiceResponse<Book>> UpdateBook(Book product);
    Task<ServiceResponse<bool>> DeleteBook(int id);


}