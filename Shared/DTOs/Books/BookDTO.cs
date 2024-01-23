using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs.Books
{
	public class BookDTO
	{
		public int Id { get; set; }
		public string Title {  get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;

		public string AuthorName { get; set; } = string.Empty;
		public string AuthorUrl { get; set; } = string.Empty;
		public decimal Price { get; set; }

	}
}
