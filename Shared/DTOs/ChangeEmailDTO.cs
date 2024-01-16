using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs
{
    public class ChangeEmailDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
