using Ecommerce.Shared.Books;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Orders
{
	public class OrderItem
	{
		public Order Order { get; set; }
		public int OrderId { get; set; }

		public Book Book { get; set; }
		public int BookId { get; set; }

		public BookType BookType { get; set; }
		public int BookTypeId { get; set; }

		public int Quantity { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalPrice { get; set; }


	}
}
