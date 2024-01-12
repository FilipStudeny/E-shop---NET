using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;

namespace Ecommerce.Client.Services.BookService
{
    public interface IBookService
    {
        event Action? OnChange;
        List<Book> Books { get; set; }
        List<FeaturedBook> FeaturedBooks { get; set; }

        int CurrentPage { get; set; }
        int PageCount { get; set; } 
        string Message { get; set; }

        Task GetBooks(int page);
		Task GetBooksByCategory(int page, int category);

		Task<ServiceResponse<Book>> GetBook(int id);
        Task GetFeaturedBooks();
        Task<List<string>> GetSuggestedBooks(string search);
    }
}
