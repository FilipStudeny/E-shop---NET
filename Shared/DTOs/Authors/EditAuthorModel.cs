using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs.Authors
{
	public class EditAuthorModel
	{
		public int Id { get; set; }
		[Required]
		[StringLength(200, ErrorMessage = "Author must have a name", MinimumLength = 1)]
		public string Name { get; set; } = string.Empty;
		public string Url { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;

		[Required]
		[StringLength(1000, ErrorMessage = "Biography must have lenght betwean 25 and 1000 words", MinimumLength = 25)]
		public string Biography { get; set; } = string.Empty;
		public bool Visible { get; set;} = true;
		
	}
}
