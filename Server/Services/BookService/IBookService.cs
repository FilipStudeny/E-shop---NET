using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Books;

namespace Ecommerce.Server.Services.BookService
{
    public interface IBookService
    {
        Task<ServiceResponse<List<BookDTO>>> GetBooks(int page, bool getAll = false, int numberOfItems = 5);
        Task<ServiceResponse<Book>> GetBook(int id);
        Task<ServiceResponse<List<BookDTO>>> GetFeaturedBooks(int page, int numberOfItems = 5);
        Task<ServiceResponse<List<string>>> GetBookSuggestions(string search);
        Task<ServiceResponse<List<BookDTO>>> GetBooksByCategory(int id, int page, int numberOfItems = 5);
		Task<ServiceResponse<List<Book>>> GetBooksBySeries(int seriesId, int page);

        Task<ServiceResponse<bool>> CreateBook(EditBookModel editBookModel);
		Task<ServiceResponse<bool>> UpdateBook(EditBookModel editBookModel);

		Task<List<Book>> GetBooksInSeries(int seriesId);
		Task<List<Book>> GetBooksByAuthor(int authorId);


    }
}
