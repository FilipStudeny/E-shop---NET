using Eshop.Shared.Models.Books;
using Eshop.Shared.Models.ProductModels;

namespace Eshop.Shared.DTOs.Books;

public class FeaturedBookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string DefaultImage { get; set; } = string.Empty;
    public List<Image>? Images { get; set; } = null;
    public Author? Author { get; set; }
    public List<BookVariant> Variants { get; set; } = new (); 

}