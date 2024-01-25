using Ecommerce.Server.Database;
using Ecommerce.Shared;
using Ecommerce.Shared.DTOs.Authors;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services.BookTypeService
{
	public class BookTypeService : IBookTypeService
	{
		private readonly DataContext dataContext;

		public BookTypeService(DataContext dataContext)
        {
			this.dataContext = dataContext;
		}

        public async Task<ServiceResponse<List<DataSelectDTO>>> GetBookTypes()
		{
			var typesFromDb = await dataContext.BookTypes.ToListAsync();
			if (typesFromDb == null)
			{
				return new ServiceResponse<List<DataSelectDTO>>
				{
					Success = false,
					Message = "No book types found."
				};
			}

			var types = typesFromDb.Select(type => new DataSelectDTO
			{
				Id = type.Id,
				Name = type.Name
			}).ToList();

			return new ServiceResponse<List<DataSelectDTO>>
			{
				Data = types
			};
		}
	}
}
