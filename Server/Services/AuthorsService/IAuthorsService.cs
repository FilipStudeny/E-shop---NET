using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared;

namespace Ecommerce.Server.Services.AuthorsService
{
	public interface IAuthorsService
	{
		Task<ServiceResponse<List<Author>>> GetAuthors(int page);
		Task<ServiceResponse<AuthorDTO>> GetAuthor(string name);
		Task<ServiceResponse<bool>> AddAuthor(Author author);
		Task<ServiceResponse<bool>> UpdateAuthor(Author author);
		Task<ServiceResponse<bool>> DeleteAuthor(int id);


	}
}
