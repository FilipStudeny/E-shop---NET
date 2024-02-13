using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Orders
{
	public class OrderBookDetailDTO
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string BookType { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }
	}
}
