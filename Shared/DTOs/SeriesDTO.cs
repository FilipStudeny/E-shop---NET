using Ecommerce.Shared.Books;


namespace Ecommerce.Shared.DTOs
{
	public class SeriesDTO
	{
		public Ecommerce.Shared.Books.Series Series { get; set; } = new();
		public Author? Author { get; set; }
		public List<Book>? Books { get; set; }

	}
}
