using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs.Authors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs.Series
{
	public class EditSeriesModel
	{
		public int Id { get; set; }
		[Required]
		[StringLength(100, ErrorMessage = "Name must have lenght betwean 1 and 100 words", MinimumLength = 1)]
		public string Name { get; set; } = string.Empty;
		public string Url { get; set; } = string.Empty;
		public DataSelectDTO Author { get; set; } = new();

		[Required]
		[StringLength(1000, ErrorMessage = "Description must have lenght betwean 25 and 1000 words", MinimumLength = 25)]
		public string Description { get; set; } = string.Empty;
		public bool Visible { get; set; } = true;

	}
}
