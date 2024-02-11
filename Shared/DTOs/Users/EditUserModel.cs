using Ecommerce.Shared.DTOs.Authors;
using Ecommerce.Shared.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs.Users
{
	public class EditUserModel
	{
		public int Id { get; set; }

		[EmailAddress]
		public string Email { get; set; } = string.Empty;
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
		public DateTime RegistrationDate { get; set; } = DateTime.Now;

		public DataSelectDTO Role { get; set; } = new();

	}
}
