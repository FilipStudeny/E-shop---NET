using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs.Authors
{
	public class DataSelectDTO
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
        public override string ToString() => Name;


	}
}
