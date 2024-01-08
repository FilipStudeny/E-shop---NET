using System.Net.Http.Json;
using Eshop.Shared.Models.Books;

namespace Eshop.Client.Services.BookTypeService;

public class BookTypeService : IBookTypeService
{
    private readonly HttpClient _httpClient;
    public event Action? OnChange;
    public List<BookType>? BookTypes { get; set; }

    public BookTypeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task GetBookTypes()
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<BookType>>>("api/booktype");
        BookTypes = response?.Data ?? new List<BookType>();
    }

    public async Task AddBookType(BookType bookType)
    {
        var response = await _httpClient.PostAsJsonAsync("api/booktype", bookType);
        var responseData = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<BookType>>>())?.Data;
        BookTypes = responseData ?? new List<BookType>();
        OnChange?.Invoke();    
    }

    public async Task UpdateBookType(BookType bookType)
    {
        var response = await _httpClient.PutAsJsonAsync("api/booktype", bookType);
        var responseData = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<BookType>>>())?.Data;
        BookTypes = responseData ?? new List<BookType>();
        OnChange?.Invoke();
    }

    public BookType CreateNewBookType()
    {
        var newType = new BookType() { IsNew = true, Editing = true };
        BookTypes?.Add(newType);
        OnChange?.Invoke();
        return newType;    
    }
}