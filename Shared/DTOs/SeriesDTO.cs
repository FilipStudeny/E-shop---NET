using Ecommerce.Shared.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs
{
	public class SeriesDTO
	{
		public Series Series { get; set; } = new();
		public Author? Author { get; set; }
		public List<Book>? Books { get; set; }

	}
}
