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
		public required byte[] PasswordHash { get; set; }
		public required byte[] PasswordSalt { get; set; }
		public DateTime RegistrationDate { get; set; } = DateTime.Now;

		public Role Role { get; set; } = new();
		public int RoleId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;

    }
}
