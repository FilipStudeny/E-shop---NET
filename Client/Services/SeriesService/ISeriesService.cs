using Ecommerce.Shared.DTOs;
using Ecommerce.Shared;
using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs.Authors;
using Ecommerce.Shared.DTOs.Books;
using Ecommerce.Shared.DTOs.Series;

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


		Task<ServiceResponse<bool>> Add(EditSeriesModel editSeriesModel);
		Task<ServiceResponse<bool>> Update(EditSeriesModel editSeriesModel);

	}
}
