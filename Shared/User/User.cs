using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.User
{
	public class User
	{
		public int Id { get; set; }
		public string Email { get; set; } = string.Empty;
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
		public DateTime RegistrationDate { get; set; } = DateTime.Now;

		public Role Role { get; set; }
		public int RoleId { get; set; }

	}
}
