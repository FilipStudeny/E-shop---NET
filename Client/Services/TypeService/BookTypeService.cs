using Ecommerce.Shared;
using Ecommerce.Shared.DTOs.Authors;
using System.Net.Http;
using System.Net.Http.Json;

namespace Ecommerce.Client.Services.TypeService
{
	public class BookTypeService : IBookTypeService
	{
		private readonly HttpClient httpClient;

		public event Action? OnChange;

        public BookTypeService(HttpClient httpClient)
        {
			this.httpClient = httpClient;
		}

        public async Task<ServiceResponse<List<DataSelectDTO>>> GetBookTypes()
		{
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<DataSelectDTO>>>($"api/type/");
			return response!;
		}
	}
}
