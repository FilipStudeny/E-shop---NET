using Ecommerce.Shared.Books;
using Ecommerce.Shared;
using Ecommerce.Shared.DTOs;

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
		Task<ServiceResponse<AuthorDTO>> GetAuthor(string Name);
	}
}
