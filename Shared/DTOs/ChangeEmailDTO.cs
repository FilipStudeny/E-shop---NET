using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs
{
    public class ChangeEmailDTO
    {
        public int UserId { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;
    }
}
