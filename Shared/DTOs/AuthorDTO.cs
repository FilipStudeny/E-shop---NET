using Ecommerce.Shared.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs
{
    public class AuthorDTO
    {
        public Author Author { get; set; } = new();
        public List<Book> Books { get; set; } = new();
        public List<Series> Series { get; set; } = new();
    }
}
