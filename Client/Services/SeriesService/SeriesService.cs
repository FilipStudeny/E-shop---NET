using Ecommerce.Client.Pages;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using System.Net.Http.Json;

namespace Ecommerce.Client.Services.SeriesService
{
	public class SeriesService : ISeriesService
	{
		private readonly HttpClient httpClient;

		public List<Series> Series { get; set; } = new();
		public int CurrentPage { get; set; }
		public int PageCount { get; set; }
		public string Message { get; set; }

		public event Action? OnChange;

        public SeriesService(HttpClient httpClient)
        {
			this.httpClient = httpClient;
		}

        public async Task GetSeries(int page)
		{
			var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<Series>>>($"api/series");
			if (response is { Data: not null })
			{
				Series = response.Data;
			}

			if (Series.Count == 0)
			{
				Message = "No series found.";
			}

			OnChange?.Invoke();
		}
	}
}
