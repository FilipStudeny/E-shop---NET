using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Orders
{
	public class OrderDTO
	{
		public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalPrice { get; set; }
		public string Product { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
	}
}
