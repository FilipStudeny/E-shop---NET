using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using System.Net.Http.Json;

namespace Ecommerce.Client.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly HttpClient httpClient;

        public event Action? OnChange;

        public List<Book> Books { get; set; } = new();
        public List<FeaturedBook> FeaturedBooks { get; set; } = new();
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public string Message { get; set; } = "Loading ...";

        public BookService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task GetBooks(int page)
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<Book>>>($"api/books/{page}");
            if (response is { Data: not null })
            {
                Books = response.Data;
                CurrentPage = response.CurrentPage;
                PageCount = response.NumberOfPages;
            }

            if (Books.Count == 0)
            {
                Message = "No products found.";
            }

            OnChange?.Invoke();
        }

        public async Task GetFeaturedBooks()
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<FeaturedBook>>>("api/books/featured");
            if(response is { Data: not null })
            {
                FeaturedBooks = response.Data;
            }

            if(FeaturedBooks.Count == 0)
            {
                Message = "No products found.";
            }

            OnChange?.Invoke();
        }

        public Task<ServiceResponse<Book>> GetBook(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetSuggestedBooks(string search)
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/books/suggestions/{search}");
            return response!.Data!;
            
        }
    }
}
