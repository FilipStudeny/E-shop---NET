using System.Net.Http.Json;
using Eshop.Shared.DTOs;
using Eshop.Shared.DTOs.Books;
using Eshop.Shared.Models.Books;

namespace Eshop.Client.Services.BookService;

public class BookService : IBookService
{
    private readonly HttpClient _httpClient;
    public event Action? OnChange;
    public string Message { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public string LastSearch { get; set; }
    public List<Book> Books { get; set; } = new();
    public List<FeaturedBookDto> FeaturedBooks { get; set; } = new();
    public List<Book> AdminBooks { get; set; } = new();

    public BookService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task GetBooksByCategory(string category)
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Book>>>($"api/book/category/{category}");;

        if (response is { Data: not null })
        {
            Books = response.Data;
        }

        CurrentPage = 1;
        PageCount = 0;
        if (Books.Count == 0)
        {
            Message = "No books found.";
        }
        OnChange?.Invoke();
    }

    public async Task GetFeaturedBooks()
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<FeaturedBookDto>>>("api/book/featured");
        if (response is { Data: not null })
        {
            FeaturedBooks = response.Data;
        }
        
        if (FeaturedBooks is { Count: 0 })
        {
            Message = "No featured books found.";
        }
        OnChange?.Invoke();
    }

    public async Task<ServiceResponse<Book>?> GetBook(int id)
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<Book>>($"api/book/{id}");
        return response;
    }

    public async Task SearchForBooks(string search, int page)
    {
        LastSearch = search;
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<BookSearchDto>>($"api/book/search/{search}/{page}");

        if (response is { Data: not null })
        {
            Books = response.Data.Books;
            CurrentPage = response.Data.CurrentPage;
            PageCount = response.Data.Pages;
        }

        if (Books.Count == 0) Message = "No products found.";
        OnChange?.Invoke();
        
    }

    public async Task<List<string>> GetBookSearchSuggestion(string search)
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/book/suggestions/{search}");
        return response?.Data!;
        
    }

    public async Task GetAdminBooks()
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Book>>>($"api/book/admin");
        AdminBooks = response?.Data!;
        CurrentPage = 1;
        PageCount = 0;

        if (AdminBooks is { Count: 0 })
        {
            Message = "No products found";
        }
    }

    public async Task<Book?> CreatBooks(Book product)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/book/admin", product);
        var responseData = (await response.Content.ReadFromJsonAsync<ServiceResponse<Book>>())?.Data;
        return responseData ?? null;
    }

    public async Task<Book?> UpdateBook(Book product)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/book/admin", product);
        var responseData =  (await response.Content.ReadFromJsonAsync<ServiceResponse<Book>>())?.Data;
        return responseData ?? null;
    }

    public async Task DeleteBook(Book product)
    {
        await _httpClient.DeleteAsync($"api/book/admin/{product.Id}");
    }
}