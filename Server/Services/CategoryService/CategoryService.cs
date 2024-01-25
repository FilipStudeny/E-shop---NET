using Ecommerce.Server.Database;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Authors;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services.CategoryService
{
	public class CategoryService : ICategoryService
	{
		private readonly DataContext dataContext;

		public CategoryService(DataContext dataContext)
        {
			this.dataContext = dataContext;
		}

		public async Task<ServiceResponse<List<Category>>> GetCategories(bool getAll = false)
		{
			var categories = getAll == false ?
				await dataContext.Category.Where(category => category.Visible && !category.Deleted).ToListAsync() :
				await dataContext.Category.ToListAsync();

			if (categories == null)
			{
				return new ServiceResponse<List<Category>>
				{
					Message = "No categories found.",
					Success = false
				};
			}

			return new ServiceResponse<List<Category>> { Data = categories };
		}

		public async Task<ServiceResponse<List<DataSelectDTO>>> GetCategoryNames()
		{
			var categoryFromDb = await dataContext.Category.ToListAsync();
			if (categoryFromDb == null)
			{
				return new ServiceResponse<List<DataSelectDTO>>
				{
					Success = false,
					Message = "No books found."
				};
			}

			var categories = categoryFromDb.Select(author => new DataSelectDTO
			{
				Id = author.Id,
				Name = author.Name
			}).ToList();


			return new ServiceResponse<List<DataSelectDTO>>
			{
				Data = categories
			};
		}

		public Task<ServiceResponse<Category>> AddCategory(CategoryDTO category)
		{
			throw new NotImplementedException();
		}

		public Task<ServiceResponse<bool>> DeleteCategory(int categoryId)
		{
			throw new NotImplementedException();
		}


		public Task<ServiceResponse<bool>> UpdateCategory(CategoryDTO categoryDTO)
		{
			throw new NotImplementedException();
		}

		

		public async Task<ServiceResponse<Category>> GetCategoryByUrl(string url)
		{
			var category = await dataContext.Category.FirstOrDefaultAsync(c => c.Url.ToLower().Equals(url.ToLower())); 
			if(category == null)
			{
				return new ServiceResponse<Category>
				{
					Success = false,
					Message = "No category found"
				};
			}

			return new ServiceResponse<Category> { Data = category };
		}

		public async Task<int> GetCategoryCount()
		{
			return await dataContext.Category.CountAsync(category => !category.Deleted && category.Visible);
			
		}
	}
}
