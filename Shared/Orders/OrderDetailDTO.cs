using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Orders
{
	public class OrderDetailDTO
	{
		public DateTime OrderDate { get; set; }
		public decimal TotalPrice { get; set; }
		public List<OrderBookDetailDTO> Books { get; set; }
	}
}
