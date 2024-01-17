using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs
{
    public class ChangePasswordDTO
    {
        public int UserId { get; set; }
		[Required]
		[StringLength(30, ErrorMessage = "Password must be atleast 8 characters long", MinimumLength = 8)]
		public string Password { get; set; } = string.Empty;

		[Required]
		[Compare(nameof(Password))]
		public string ComparePassword { get; set; } = string.Empty;
    }
}
