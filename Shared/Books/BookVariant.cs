using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Books
{
    public class BookVariant
    {
        //Composite-primary key
        [JsonIgnore] //Fix for circular reference
        public Book? Book { get; set; }
        public int BookId { get; set; }

        public BookType? BookType { get; set; }
        public int BookTypeId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; } //For sales


        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        // for form edits, not visible in the DB
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
