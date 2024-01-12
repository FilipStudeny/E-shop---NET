using Ecommerce.Shared.Books;
using Ecommerce.Shared;

namespace Ecommerce.Client.Services.AuthorsService
{
	public interface IAuthorsService
	{
		event Action? OnChange;
		List<Author> Authors { get; set; }

		int CurrentPage { get; set; }
		int PageCount { get; set; }
		string Message { get; set; }

		Task<bool> GetAuthors(int Page);
		Task<ServiceResponse<Author>> GetAuthor(string Name);
	}
}
