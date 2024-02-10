using Ecommerce.Shared.Books;
using Ecommerce.Shared.DTOs.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs.Books
{
	public class EditBookModel
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string ShortDescription { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public List<Image> Images { get; set; } = new();
		public List<BookVariant>? Variants { get; set; } = new();
		public DateTime ReleaseDate { get; set; }

		public DataSelectDTO Author { get; set; } = new();
		public DataSelectDTO Series { get; set; } = new();
		public DataSelectDTO Category { get; set; } = new();

		public string Isbn { get; set; } = string.Empty;
		public int CopiesInStore { get; set; }
		public int PageCount { get; set; }
		public int SeriesOrder { get; set; }

		public bool Featured { get; set; } = false;
		public bool Visible { get; set; } = true;


	}
}
