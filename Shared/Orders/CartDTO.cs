using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Orders
{
	public class CartDTO
	{
		public int BookId { get; set; }
		public string BookTitle { get; set; } = string.Empty;

		public int BookTypeId { get; set; }
		public string BookType { get; set; } = string.Empty;

		public string Image { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}
}
