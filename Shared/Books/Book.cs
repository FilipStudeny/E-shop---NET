﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Books
{
    public class Book
    {
        public int Id { get; set; }

		public string Title { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;


		public string ShortDescription { get; set; } = string.Empty;
        public List<Image> Images { get; set; } = new();
        public string DefaultImageUrl { get; set; } = string.Empty;

		public Author? Author { get; set; }
        public int AuthorId { get; set; }

		public Category? Category { get; set; }
        public int CategoryId { get; set; }

        public Series? Series { get; set; }
        public int SeriesId { get; set; }
        public int SeriesOrder { get; set; }

        public List<BookVariant>? Variants { get; set; } = new();

		//Book data
		[Required]
		[Range(1, 5000, ErrorMessage = "PageCount should be between 1 and 5000")]
		public int PageCount { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime ReleaseDate { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "ISBN is too short or empty", MinimumLength = 5)]
		public string Isbn { get; set; } = string.Empty;
        public bool Featured { get; set; }

		public int CopiesInStore { get; set; }


        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        // Client froms data
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
