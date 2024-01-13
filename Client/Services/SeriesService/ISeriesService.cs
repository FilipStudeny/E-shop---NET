using Ecommerce.Shared.DTOs;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;

namespace Ecommerce.Client.Services.SeriesService
{
	public interface ISeriesService
	{
		event Action? OnChange;
		List<Series> Series { get; set; }

		int CurrentPage { get; set; }
		int PageCount { get; set; }
		string Message { get; set; }

		Task GetSeries(int page);
		Task<ServiceResponse<SeriesDTO>> GetSingleSeries(string name);

	}
}
