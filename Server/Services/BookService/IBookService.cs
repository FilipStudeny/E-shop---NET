using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;

namespace Ecommerce.Server.Services.BookService
{
    public interface IBookService
    {
        Task<ServiceResponse<List<Book>>> GetBooks(int page);
        Task<ServiceResponse<Book>> GetBook(int id);
        Task<ServiceResponse<List<FeaturedBook>>> GetFeaturedBooks();
        Task<ServiceResponse<List<string>>> GetBookSuggestions(string search);
		Task<ServiceResponse<List<Book>>> GetBooksByCategory(int id, int page);
		Task<ServiceResponse<List<Book>>> GetBooksBySeries(int seriesId, int page);


		Task<List<Book>> GetBooksInSeries(int seriesId);
		Task<List<Book>> GetBooksByAuthor(int authorId);


    }
}
