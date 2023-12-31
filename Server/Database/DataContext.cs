﻿using Eshop.Shared.Models;
using Eshop.Shared.Models.Books;
using Eshop.Shared.Models.Cart;
using Eshop.Shared.Models.Order;
using Eshop.Shared.Models.ProductModels;
using Eshop.Shared.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Database;

public class DataContext : DbContext
{
    //Books
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookType> BookTypes { get; set; }
    public DbSet<BookVariant> BookVariants { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<BookVariant>().HasKey(book => new
        {
            book.BookId,
            book.BookTypeId
        });

        modelBuilder.Entity<BookType>().HasData(
            new BookType { Id = 1, Name = "Paperback" },
            new BookType { Id = 2, Name = "Ebook" },
            new BookType { Id = 3, Name = "Audiobook" }
        );

        modelBuilder.Entity<Author>().HasData(
            new Author
            {
                Id = 1,
                Name = "J.K. Rowling",
                BiographyText =
                    "Joanne Rowling, better known by her pen name J.K. Rowling, is a British author, best known for writing the Harry Potter series."
            },
            new Author
            {
                Id = 2,
                Name = "George R.R. Martin",
                BiographyText =
                    "George Raymond Richard Martin, often referred to as GRRM, is an American novelist and short story writer, known for his series A Song of Ice and Fire."
            },
            new Author
            {
                Id = 3,
                Name = "J.R.R. Tolkien",
                BiographyText =
                    "John Ronald Reuel Tolkien was an English writer, poet, and university professor. He is best known as the author of the classic high-fantasy works The Hobbit, The Lord of the Rings, and The Silmarillion."
            }
        );
        modelBuilder.Entity<Series>().HasData(
            new Series
            {
                Id = 1,
                Name = "Harry Potter",
                Description = "Harry potter series about wierd hobo magic castle"
            },
            new Series
            {
                Id = 2,
                Name = "Song of Ice and Fire",
                Description = "Hobo wars betwean kingdoms"
            },
            new Series
            {
                Id = 3,
                Name = "Hobbit",
                Description = "Hobo goes on adventures with strange ring"
            }
        );

        modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Harry Potter and the Philosopher's Stone",
                    Description = "The first book in the Harry Potter series.",
                    ShortDescription = "A young wizard's journey begins.",
                    AuthorId = 1, // Corresponds to J.K. Rowling
                    CategoryId = 1, // Assuming you have categories with IDs
                    SeriesId = 1, // Assuming you have series with IDs
                    SeriesOrder = 1,
                    PageCount = 320,
                    DateAdded = DateTime.Now,
                    DefaultImage = "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/h/harry-potter-7-a-dary-smrti-9788055143132.jpg",
                    ReleaseDate = new DateTime(1997, 6, 26),
                    Isbn = "9780590353427",
                    Featured = true,
                    Deleted = false,
                    CopiesInStore = 100,
                },
                new Book
                {
                    Id = 2,
                    Title = "A Game of Thrones",
                    Description = "The first book in A Song of Ice and Fire series.",
                    ShortDescription = "Power struggles in the Seven Kingdoms.",
                    AuthorId = 2, // Corresponds to George R.R. Martin
                    CategoryId = 2, // Assuming different category ID
                    SeriesId = 2,
                    SeriesOrder = 1,
                    PageCount = 694,
                    DateAdded = DateTime.Now,
                    DefaultImage = "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/h/hra-o-truny-9788025722824.jpg",
                    ReleaseDate = new DateTime(1996, 8, 6),
                    Isbn = "9780553103540",
                    Featured = true,
                    Deleted = false,
                    CopiesInStore = 150,
                },
                new Book
                {
                    Id = 3,
                    Title = "The Hobbit",
                    Description = "A fantasy novel by J.R.R. Tolkien.",
                    ShortDescription = "Bilbo Baggins' unexpected journey.",
                    AuthorId = 3, // Corresponds to J.R.R. Tolkien
                    CategoryId = 1, // Assuming different category ID
                    SeriesId = 3, // Assuming different series ID
                    SeriesOrder = 1,
                    PageCount = 310,
                    DateAdded = DateTime.Now,
                    DefaultImage = "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/387191598/10.jpg",
                    ReleaseDate = new DateTime(1937, 9, 21),
                    Isbn = "9780345534835",
                    Featured = true,
                    Deleted = false,
                    CopiesInStore = 80,
                }
        );

        modelBuilder.Entity<BookVariant>().HasData(
            new BookVariant()
            {
                BookId = 1,
                BookTypeId = 1,
                Price = 9.99m,
                OriginalPrice = 19.99m
            },
            new BookVariant()
            {
                BookId = 1,
                BookTypeId = 2,
                Price = 25.99m,
                OriginalPrice = 19.99m
            },
            new BookVariant()
            {
                BookId = 2,
                BookTypeId = 2,
                Price = 9.99m,
                OriginalPrice = 19.99m
            },
            new BookVariant()
            {
                BookId = 2,
                BookTypeId = 3,
                Price = 19.99m,
                OriginalPrice = 25.99m
            },
            new BookVariant()
            {
                BookId = 2,
                BookTypeId = 1,
                Price = 25.99m,
                OriginalPrice = 30.99m
            },
            new BookVariant()
            {
                BookId = 3,
                BookTypeId = 2,
                Price = 9.99m,
                OriginalPrice = 19.99m
            },
            new BookVariant()
            {
                BookId = 3,
                BookTypeId = 1,
                Price = 9.99m,
                OriginalPrice = 19.99m
            }
        );

        
        //Composite key setup
        modelBuilder.Entity<CartItem>().HasKey(cart => new
        {
            cart.UserId,
            cart.ProductId,
            cart.ProductTypeId
        });
        
        modelBuilder.Entity<ProductVariant>().HasKey(product => new
        {
            product.ProductId, 
            product.ProductTypeId
        });
        
        modelBuilder.Entity<OrderItem>().HasKey(order => new
        {
            order.OrderId,
            order.ProductId, 
            order.ProductTypeId
        });
        
        modelBuilder.Entity<ProductType>().HasData(
            new ProductType { Id = 1, Name = "Default" },
            new ProductType { Id = 2, Name = "Paperback" },
            new ProductType { Id = 3, Name = "E-Book" },
            new ProductType { Id = 4, Name = "Audiobook" },
            new ProductType { Id = 5, Name = "Stream" },
            new ProductType { Id = 6, Name = "Blu-ray" },
            new ProductType { Id = 7, Name = "VHS" },
            new ProductType { Id = 8, Name = "PC" },
            new ProductType { Id = 9, Name = "PlayStation" },
            new ProductType { Id = 10, Name = "Xbox" }
        );
        
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Books",
                Url = "books"
            },new Category
            {
                Id = 2,
                Name = "Movies",
                Url = "movies"
            },new Category
            {
                Id = 3,
                Name = "Video games",
                Url = "video_games"
            },new Category
            {
                Id = 4,
                Name = "Phones",
                Url = "mobile_phones"
            }
        );

        //** SEED DATA **//
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Title = "The Hitchhiker's Guide to the Galaxy",
                Description =
                    "The Hitchhiker's Guide to the Galaxy[note 1] (sometimes referred to as HG2G,[1] HHGTTG,[2] H2G2,[3] or tHGttG) is a comedy science fiction franchise created by Douglas Adams. Originally a 1978 radio comedy broadcast on BBC Radio 4, it was later adapted to other formats, including stage shows, novels, comic books, a 1981 TV series, a 1984 text-based computer game, and 2005 feature film.",
                Image = "https://upload.wikimedia.org/wikipedia/en/b/bd/H2G2_UK_front_cover.jpg",
                CategoryId = 1,
                FeaturedProduct = true
            },new Product
            {
                Id = 2,
                Title = "Ready Player One",
                Description =
                    "Ready Player One is a 2011 science fiction novel, and the debut novel of American author Ernest Cline. The story, set in a dystopia in 2045, follows protagonist Wade Watts on his search for an Easter egg in a worldwide virtual reality game, the discovery of which would lead him to inherit the game creator's fortune. Cline sold the rights to publish the novel in June 2010, in a bidding war to the Crown Publishing Group (a division of Random House).[1] The book was published on August 16, 2011.[2] An audiobook was released the same day; it was narrated by Wil Wheaton, who was mentioned briefly in one of the chapters.[3][4]Ch. 20 In 2012, the book received an Alex Award from the Young Adult Library Services Association division of the American Library Association[5] and won the 2011 Prometheus Award.[6]",
                Image = "https://upload.wikimedia.org/wikipedia/en/a/a4/Ready_Player_One_cover.jpg",
                CategoryId = 1
            },new Product
            {
                Id = 3,
                Title = "Nineteen Eighty-Four",
                Description =
                    "Nineteen Eighty-Four (also stylised as 1984) is a dystopian social science fiction novel and cautionary tale written by English writer George Orwell. It was published on 8 June 1949 by Secker & Warburg as Orwell's ninth and final book completed in his lifetime. Thematically, it centres on the consequences of totalitarianism, mass surveillance and repressive regimentation of people and behaviours within society.[2][3] Orwell, a democratic socialist, modelled the totalitarian government in the novel after Stalinist Russia and Nazi Germany.[2][3][4] More broadly, the novel examines the role of truth and facts within politics and the ways in which they are manipulated.",
                Image = "https://upload.wikimedia.org/wikipedia/commons/c/c3/1984first.jpg",
                CategoryId = 1
            },new Product
            {
                Id = 4,
                CategoryId = 2,
                Title = "The Matrix",
                Description =
                    "The Matrix is a 1999 science fiction action film written and directed by the Wachowskis, and produced by Joel Silver. Starring Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss, Hugo Weaving, and Joe Pantoliano, and as the first installment in the Matrix franchise, it depicts a dystopian future in which humanity is unknowingly trapped inside a simulated reality, the Matrix, which intelligent machines have created to distract humans while using their bodies as an energy source. When computer programmer Thomas Anderson, under the hacker alias \"Neo\", uncovers the truth, he \"is drawn into a rebellion against the machines\" along with other people who have been freed from the Matrix.",
                Image = "https://upload.wikimedia.org/wikipedia/en/c/c1/The_Matrix_Poster.jpg",
            },new Product
            {
                Id = 5,
                CategoryId = 2,
                Title = "Back to the Future",
                Description =
                    "Back to the Future is a 1985 American science fiction film directed by Robert Zemeckis. Written by Zemeckis and Bob Gale, it stars Michael J. Fox, Christopher Lloyd, Lea Thompson, Crispin Glover, and Thomas F. Wilson. Set in 1985, the story follows Marty McFly (Fox), a teenager accidentally sent back to 1955 in a time-traveling DeLorean automobile built by his eccentric scientist friend Doctor Emmett \"Doc\" Brown (Lloyd). Trapped in the past, Marty inadvertently prevents his future parents' meeting—threatening his very existence—and is forced to reconcile the pair and somehow get back to the future.",
                Image = "https://upload.wikimedia.org/wikipedia/en/d/d2/Back_to_the_Future.jpg",
            },new Product
            {
                Id = 6,
                CategoryId = 2,
                Title = "Toy Story",
                Description =
                    "Toy Story is a 1995 American computer-animated comedy film produced by Pixar Animation Studios and released by Walt Disney Pictures. The first installment in the Toy Story franchise, it was the first entirely computer-animated feature film, as well as the first feature film from Pixar. The film was directed by John Lasseter (in his feature directorial debut), and written by Joss Whedon, Andrew Stanton, Joel Cohen, and Alec Sokolow from a story by Lasseter, Stanton, Pete Docter, and Joe Ranft. The film features music by Randy Newman, was produced by Bonnie Arnold and Ralph Guggenheim, and was executive-produced by Steve Jobs and Edwin Catmull. The film features the voices of Tom Hanks, Tim Allen, Don Rickles, Wallace Shawn, John Ratzenberger, Jim Varney, Annie Potts, R. Lee Ermey, John Morris, Laurie Metcalf, and Erik von Detten. Taking place in a world where anthropomorphic toys come to life when humans are not present, the plot focuses on the relationship between an old-fashioned pull-string cowboy doll named Woody and an astronaut action figure, Buzz Lightyear, as they evolve from rivals competing for the affections of their owner, Andy Davis, to friends who work together to be reunited with Andy after being separated from him.",
                Image = "https://upload.wikimedia.org/wikipedia/en/1/13/Toy_Story.jpg",
            },new Product
            {
                Id = 7,
                CategoryId = 3,
                Title = "Half-Life 2",
                Description =
                    "Half-Life 2 is a 2004 first-person shooter game developed and published by Valve. Like the original Half-Life, it combines shooting, puzzles, and storytelling, and adds features such as vehicles and physics-based gameplay.",
                Image = "https://upload.wikimedia.org/wikipedia/en/2/25/Half-Life_2_cover.jpg",
            },new Product
            {
                Id = 8,
                CategoryId = 3,
                Title = "Diablo II",
                Description =
                    "Diablo II is an action role-playing hack-and-slash computer video game developed by Blizzard North and published by Blizzard Entertainment in 2000 for Microsoft Windows, Classic Mac OS, and macOS.",
                Image = "https://upload.wikimedia.org/wikipedia/en/d/d5/Diablo_II_Coverart.png",
                FeaturedProduct = true
            },new Product
            {
                Id = 9,
                CategoryId = 3,
                Title = "Day of the Tentacle",
                Description =
                    "Day of the Tentacle, also known as Maniac Mansion II: Day of the Tentacle, is a 1993 graphic adventure game developed and published by LucasArts. It is the sequel to the 1987 game Maniac Mansion.",
                Image = "https://upload.wikimedia.org/wikipedia/en/7/79/Day_of_the_Tentacle_artwork.jpg",
            },new Product
            {
                Id = 10,
                CategoryId = 3,
                Title = "Xbox",
                Description =
                    "The Xbox is a home video game console and the first installment in the Xbox series of video game consoles manufactured by Microsoft.",
                Image = "https://upload.wikimedia.org/wikipedia/commons/4/43/Xbox-console.jpg",
                FeaturedProduct = true
            },new Product
            {
                Id = 11,
                CategoryId = 3,
                Title = "Super Nintendo Entertainment System",
                Description =
                    "The Super Nintendo Entertainment System (SNES), also known as the Super NES or Super Nintendo, is a 16-bit home video game console developed by Nintendo that was released in 1990 in Japan and South Korea.",
                Image = "https://upload.wikimedia.org/wikipedia/commons/e/ee/Nintendo-Super-Famicom-Set-FL.jpg",
            }
        );
        
        modelBuilder.Entity<ProductVariant>().HasData(
                new ProductVariant
                {
                    ProductId = 1,
                    ProductTypeId = 2,
                    Price = 9.99m,
                    OriginalPrice = 19.99m
                },
                new ProductVariant
                {
                    ProductId = 1,
                    ProductTypeId = 3,
                    Price = 7.99m
                },
                new ProductVariant
                {
                    ProductId = 1,
                    ProductTypeId = 4,
                    Price = 19.99m,
                    OriginalPrice = 29.99m
                },
                new ProductVariant
                {
                    ProductId = 2,
                    ProductTypeId = 2,
                    Price = 7.99m,
                    OriginalPrice = 14.99m
                },
                new ProductVariant
                {
                    ProductId = 3,
                    ProductTypeId = 2,
                    Price = 6.99m
                },
                new ProductVariant
                {
                    ProductId = 4,
                    ProductTypeId = 5,
                    Price = 3.99m
                },
                new ProductVariant
                {
                    ProductId = 4,
                    ProductTypeId = 6,
                    Price = 9.99m
                },
                new ProductVariant
                {
                    ProductId = 4,
                    ProductTypeId = 7,
                    Price = 19.99m
                },
                new ProductVariant
                {
                    ProductId = 5,
                    ProductTypeId = 5,
                    Price = 3.99m,
                },
                new ProductVariant
                {
                    ProductId = 6,
                    ProductTypeId = 5,
                    Price = 2.99m
                },
                new ProductVariant
                {
                    ProductId = 7,
                    ProductTypeId = 8,
                    Price = 19.99m,
                    OriginalPrice = 29.99m
                },
                new ProductVariant
                {
                    ProductId = 7,
                    ProductTypeId = 9,
                    Price = 69.99m
                },
                new ProductVariant
                {
                    ProductId = 7,
                    ProductTypeId = 10,
                    Price = 49.99m,
                    OriginalPrice = 59.99m
                },
                new ProductVariant
                {
                    ProductId = 8,
                    ProductTypeId = 8,
                    Price = 9.99m,
                    OriginalPrice = 24.99m,
                },
                new ProductVariant
                {
                    ProductId = 9,
                    ProductTypeId = 8,
                    Price = 14.99m
                },
                new ProductVariant
                {
                    ProductId = 10,
                    ProductTypeId = 1,
                    Price = 159.99m,
                    OriginalPrice = 299m
                },
                new ProductVariant
                {
                    ProductId = 11,
                    ProductTypeId = 1,
                    Price = 79.99m,
                    OriginalPrice = 399m
                }
            );
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Image> Images { get; set; }
    


}
