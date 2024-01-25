using Ecommerce.Shared.DTOs;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs.Authors;

namespace Ecommerce.Client.Services.SeriesService
{
	public interface ISeriesService
	{
		event Action? OnChange;
		List<Series> Series { get; set; }
		int SeriesCount { get; set; }

		int CurrentPage { get; set; }
		int PageCount { get; set; }
		string Message { get; set; }

		Task GetSeries(int page, bool getAll = false);
		Task<ServiceResponse<SeriesDTO>> GetSingleSeries(string name);
		Task<ServiceResponse<List<DataSelectDTO>>> GetAllSeriesNames();

	}
}
