using Eshop.Shared.Models.Books;

namespace Eshop.Server.Services.BookTypeService;

public interface IBookTypeService
{
    Task<ServiceResponse<List<BookType>>> GetBookTypes();
    Task<ServiceResponse<List<BookType>>> AddBookTypes(BookType bookType);
    Task<ServiceResponse<List<BookType>>> UpdateBookType(BookType bookType);
}