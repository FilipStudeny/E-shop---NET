﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Books
{
    public class Image
    {
        public int Id { get; set; }
        public string Data { get; set; } = string.Empty;
        public int BookId { get; set; }
    }
}
