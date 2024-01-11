using Ecommerce.Shared.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs
{
    public class FeaturedBook
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string UrlImage { get; set; } = string.Empty;
        public Image? Image { get; set; }
        public string Author { get; set; } = string.Empty;
        public string AuthorUrl { get; set; } = string.Empty;
    }
}
