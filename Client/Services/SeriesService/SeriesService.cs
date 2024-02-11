using Ecommerce.Client.Pages;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs;
using Ecommerce.Shared.DTOs.Authors;
using Ecommerce.Shared.DTOs.Books;
using Ecommerce.Shared.DTOs.Series;
using MudBlazor;
using System.Net.Http.Json;

namespace Ecommerce.Client.Services.SeriesService
{
	public class SeriesService : ISeriesService
	{
		private readonly HttpClient httpClient;

		public List<Series> Series { get; set; } = new();

		public int SeriesCount { get; set; }
		public int CurrentPage { get; set; }
		public int PageCount { get; set; }
		public string Message { get; set; }

		public event Action? OnChange;

        public SeriesService(HttpClient httpClient)
        {
			this.httpClient = httpClient;
		}

        public async Task GetSeries(int page, bool getAll = false)
		{
			var response = getAll == false ?
				await httpClient.GetFromJsonAsync<ServiceResponse<List<Series>>>($"api/series/{page}"):
				await httpClient.GetFromJsonAsync<ServiceResponse<List<Series>>>($"api/series/admin/{page}");

			if (response is { Data: not null })
			{
				Series = response.Data;
				CurrentPage = response.CurrentPage;
				PageCount = response.NumberOfPages;
			}

			if (Series.Count == 0)
			{
				Message = "No series found.";
			}

			OnChange?.Invoke();
		}

		public async Task<ServiceResponse<SeriesDTO>> GetSingleSeries(string name)
		{
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<SeriesDTO>>($"api/series/series/{name}");
			return response!;
			
		}

		public async Task<ServiceResponse<List<DataSelectDTO>>> GetAllSeriesNames()
		{
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<DataSelectDTO>>>($"api/series/admin/all");
			return response!;

		}

		public async Task<ServiceResponse<bool>> Add(EditSeriesModel editSeriesModel)
		{
			var response = await httpClient.PostAsJsonAsync("api/series/admin/add", editSeriesModel);
			var responseContent = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
			if (responseContent == null)
			{
				return new ServiceResponse<bool> { Success = false, Message = "Error creating new series" };
			}
			return responseContent;
		}

		public async Task<ServiceResponse<bool>> Update(EditSeriesModel editSeriesModel)
		{
			var response = await httpClient.PutAsJsonAsync("api/series/admin/update", editSeriesModel);
			var responseData = (await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>());
			if (responseData == null)
			{
				return new ServiceResponse<bool> { Success = false, Message = "Failed to update series, try again later" };
			}
			return responseData;
		}
	}
}
