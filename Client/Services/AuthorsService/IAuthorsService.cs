using Ecommerce.Shared.Books;
using Ecommerce.Shared;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Authors;

namespace Ecommerce.Client.Services.AuthorsService
{
	public interface IAuthorsService
	{
		event Action? OnChange;
		List<Author> Authors { get; set; }

		int CurrentPage { get; set; }
		int PageCount { get; set; }
		string Message { get; set; }
		int AuthorsCount { get; set; }

		Task<bool> GetAuthors(int Page, bool getAll = false);
		Task<ServiceResponse<AuthorDTO>> GetAuthor(string Name);
		Task<ServiceResponse<List<DataSelectDTO>>> GetAllAuthorsNames();

		Task<ServiceResponse<EditAuthorModel>> GetAuthorForEdit(string name);
		Task<ServiceResponse<bool>> UpdateAuthor(EditAuthorModel editAuthorModel, string urlName);
		Task<ServiceResponse<bool>> CreateAuthor(EditAuthorModel editAuthorModel);


	}
}
