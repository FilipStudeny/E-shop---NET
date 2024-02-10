using Ecommerce.Client.Services.CategoryService;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Books;
using System.Net.Http.Json;

namespace Ecommerce.Client.Services.BookService
{
	public class BookService : IBookService
	{
		private readonly HttpClient httpClient;

		public event Action? OnChange;
		public List<BookDTO> Books { get; set; } = new();

		public int CurrentPage { get; set; }
		public int PageCount { get; set; }
		public string Message { get; set; } = "Loading ...";

		public int BookCount { get; set; }
		public bool Success { get; set; }

		public BookService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task GetBooks(int page, bool evenDeleted = false)
		{

			var response = evenDeleted == false ?
				await httpClient.GetFromJsonAsync<ServiceResponse<List<BookDTO>>>($"api/books/{page}?count=5") :
				await httpClient.GetFromJsonAsync<ServiceResponse<List<BookDTO>>>($"api/books/admin/{page}?count=10");

			if (response is { Data: not null })
			{
				Books = response.Data;
				CurrentPage = response.CurrentPage;
				PageCount = response.NumberOfPages;
				BookCount = response.ItemCount;
			}

			if (Books.Count == 0)
			{
				Message = "No books found.";
			}

			OnChange?.Invoke();
		}

		public async Task GetBooksByCategory(int page, int category)
		{
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<BookDTO>>>($"api/books/category/{category}/page/{page}?count=5");
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

		public async Task GetFeaturedBooks(int page)
		{
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<BookDTO>>>($"api/books/featured/{page}?count=10");

			if (response != null)
			{
				Success = response.Success;
				Message = response.Message;
				CurrentPage = response.CurrentPage;
				PageCount = response.NumberOfPages;

				if (response is { Data: not null })
				{
					Books = response.Data;
				}


				if (Books.Count == 0)
				{
					Message = "No products found.";
				}

			}

			OnChange?.Invoke();
		}

		public async Task<ServiceResponse<Book>> GetBook(int id)
		{
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<Book>>($"api/books/book/{id}");
			return response!;

		}

		public async Task<List<string>> GetSuggestedBooks(string search)
		{
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/books/suggestions/{search}");
			return response!.Data!;

		}

		public async Task<ServiceResponse<bool>> AddBook(EditBookModel editBookModel)
		{
			var response = await httpClient.PostAsJsonAsync("api/books/admin/book/add", editBookModel);
			var responseContent = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
			if (responseContent == null)
			{
				return new ServiceResponse<bool> { Success = false, Message = "Error creating new book" };
			}
			return responseContent;
		}

		public async Task<ServiceResponse<bool>> UpdateBook(EditBookModel editBookModel)
		{
			var response = await httpClient.PutAsJsonAsync("api/books/admin/book/update", editBookModel);
			var responseData = (await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>());
			if (responseData == null)
			{
				return new ServiceResponse<bool> { Success = false, Message = "Failed to update book, try again later" };
			}
			return responseData;
		}
	}
}
