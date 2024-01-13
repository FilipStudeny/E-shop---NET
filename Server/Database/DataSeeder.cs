using Ecommerce.Shared.Books;

namespace Ecommerce.Server.Database
{
    public class DataSeeder
    {


        public List<Author> SeedAuthors()
        {
            var authors = new List<Author>
            {
                   
                new Author
                {
                    Id = 1,
                    Name = "No author",
                    BiographyText = "No author",
                    Url = "no-author"
                },
                new Author
                {
                    Id = 2,
                    Name = "J.K. Rowling",
                    BiographyText =
                        "Joanne Rowling, better known by her pen name J.K. Rowling, is a British author, best known for writing the Harry Potter series.",
                    Url = "joanne-rowling"
                },
                new Author
                {
                    Id = 3,
                    Name = "George R.R. Martin",
                    BiographyText =
                        "George Raymond Richard Martin, often referred to as GRRM, is an American novelist and short story writer, known for his series A Song of Ice and Fire.",
                    Url = "george-raymond-richard-martin"
                },
                new Author
                {
                    Id = 4,
                    Name = "J.R.R. Tolkien",
                    BiographyText =
                        "John Ronald Reuel Tolkien was an English writer, poet, and university professor. He is best known as the author of the classic high-fantasy works The Hobbit, The Lord of the Rings, and The Silmarillion.",
                    Url = "john-ronald-reuel-tolkien"
                },
                new Author
                {
                    Id = 5,
                    Name = "Agatha Christie",
                    BiographyText = "Agatha Christie was an English writer known for her detective novels, particularly those revolving around her fictional detectives Hercule Poirot and Miss Marple.",
                    Url = "agatha-christie"
                },
                new Author
                {
                    Id = 6,
                    Name = "Stephen King",
                    BiographyText = "Stephen King is an American author of horror, supernatural fiction, suspense, crime, science-fiction, and fantasy novels.",
                    Url = "stephen-king"
                    
                },
                new Author
                {
                    Id = 7,
                    Name = "Isaac Asimov",
                    BiographyText = "Renowned science fiction writer",
                    Url = "issac-asimov"
                }

            };

            return authors;
        }
        public List<Category> SeedCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Default",
                    Url = "default"
                },
                new Category
                {
                    Id = 2,
                    Name = "Fantasy",
                    Url = "fantasy"
                },
                new Category
                {
                    Id = 3,
                    Name = "Sci-fi",
                    Url = "scifi"
                },
                new Category
                {
                    Id = 4,
                    Name = "Horror",
                    Url = "horror"
                },
                new Category
                {
                    Id = 5,
                    Name = "Detective",
                    Url = "detective"
                }
            };
        }
        public List<BookType> SeedBookTypes()
        {
            return new List<BookType>
            {
                new BookType{ Id = 1 ,Name = "Paperback" },
                new BookType{ Id = 2 ,Name = "Hardcover" },
                new BookType{ Id = 3 ,Name = "Ebook" },
                new BookType{ Id = 4 ,Name = "Audiobook" }
            };
        }
        public List<Series> SeedSeries()
        {
            return new List<Series>
            {
                new Series { Id = 1 ,Name = "Stand alone", Description = "Books that are not part of any series", Url = "stand-alone", AuthorId = 1 },
                new Series { Id = 2 ,Name = "Harry Potter", Description = "The magical adventures of Harry Potter", Url = "harry-potter", AuthorId = 2 },
                new Series { Id = 3 ,Name = "A Song of Ice and Fire", Description = "Epic fantasy series by George R.R. Martin", Url = "a-song-of-ice-and-fire", AuthorId = 3 },
                new Series { Id = 4 ,Name = "The Lord of the Rings", Description = "Classic high-fantasy series by J.R.R. Tolkien", Url = "the-lord-of-the-rings", AuthorId = 4 },
                new Series { Id = 5 ,Name = "Hercule Poirot", Description = "Detective series by Agatha Christie", Url = "hercule-poirot", AuthorId = 5 },
                new Series { Id = 6 ,Name = "Stephen King Horror Novels", Description = "Horror novels by Stephen King", Url = "stephen-king-horror-novels", AuthorId = 6 },
                new Series { Id = 7 ,Name = "Foundation", Description = "Sci-fi series by Isaac Asimov", Url = "foundation", AuthorId = 7 },
            };
        }
        public List<Book> SeedBooks()
        {
            return new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "Game of Thrones",
                    Description = "Winter is coming. A classic world that best describes George R. R. Martin's epic saga A Song of Ice and Fire. The kingdoms of Westeros have a long summer, which must be followed by a harsh winter. But before that, and before the Others return, the game of thrones is being played out, and there are far fewer contenders than contenders. Perhaps the only one who doesn't want the Iron Throne is Lord Eddard Stark, guardian of the north. Therefore, he must become the mover of events that will change everything.",
                    ShortDescription = " The kingdoms of Westeros have a long summer, which must be followed by a harsh winter. But before that, and before the Others return, the game of thrones is being played out, and there are far fewer contenders than contenders.",
                    DefaultImageUrl = "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/h/hra-o-truny-9788025722824.jpg",

                    AuthorId = 3,
                    CategoryId = 2,
                    SeriesId = 3,
                    SeriesOrder = 1,

                    PageCount = 694,
                    DateAdded = DateTime.Now,
                    ReleaseDate = new DateTime(1996, 8, 1),
                    Isbn = "978-80-257-2282-4",
                    CopiesInStore = 150,
                    Featured = true,
                    Visible = true,
                    Deleted = false,
                },
                 new Book
                {
                    Id = 2,
                    Title = "A Clash of Kings",
                    Description = "The Seven Kingdoms are divided by revolt and blood feud, and winter approaches like an angry beast. Beyond the Wall, the armies of the Night's Watch are massing for an assault on the kingdom of the dead. The young, the brave, and the foolish, all compete for the throne.",
                    ShortDescription = "Winter approaches and armies gather. The battle for the throne continues...",
                    DefaultImageUrl = "https://www.knihydobrovsky.cz/kniha/stret-kralu-39500927",

                    AuthorId = 3,
                    CategoryId = 2,
                    SeriesId = 3,
                    SeriesOrder = 2,

                    PageCount = 768,
                    DateAdded = DateTime.Now,
                    ReleaseDate = new DateTime(1998, 11, 16),
                    Isbn = "978-0553579901",
                    CopiesInStore = 120,

                    Featured = false,
                    Visible = true,
                    Deleted = false,
                },
                new Book
                {
                    Id = 3,
                    Title = "A Storm of Swords",
                    Description = "The Seven Kingdoms are divided by revolt and blood feud, and winter approaches like an angry beast. Beyond the Wall, the armies of the Night's Watch are massing for an assault on the kingdom of the dead. The young, the brave, and the foolish, all compete for the throne.",
                    ShortDescription = "Winter approaches and armies gather. The battle for the throne continues...",
                    DefaultImageUrl = "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/b/boure-mecu-9788025724187_17.jpg",

                    AuthorId = 3,
                    CategoryId = 2,
                    SeriesId = 3,
                    SeriesOrder = 3,

                    PageCount = 992,
                    DateAdded = DateTime.Now,
                    ReleaseDate = new DateTime(2000, 8, 8),
                    Isbn = "978-0553573428",
                    CopiesInStore = 100,

                    Featured = true,
                    Visible = true,
                    Deleted = false,
                },
                new Book
                {
                    Id = 4,
                    Title = "The Fellowship of the Ring",
                    Description = "The first part of J.R.R. Tolkien's epic masterpiece The Lord of the Rings. The story begins with the hobbit Frodo Baggins inheriting the Ring from his uncle Bilbo. Together with his fellowship, he embarks on a dangerous journey to destroy the Ring and defeat the Dark Lord Sauron.",
                    ShortDescription = "The journey to destroy the One Ring begins...",
                    DefaultImageUrl = "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/p/pan-prstenu-spolecenstvo-prstenu-ilustrovane-vydani-9788072038299_26.jpg",

                    AuthorId = 4,
                    CategoryId = 2,
                    SeriesId = 4,
                    SeriesOrder = 1,

                    PageCount = 432,
                    DateAdded = DateTime.Now,
                    ReleaseDate = new DateTime(1954, 7, 29),
                    Isbn = "978-0618640157",
                    CopiesInStore = 130,

                    Featured = false,
                    Visible = true,
                    Deleted = false,
                },
                new Book
                {
                    Id = 5,
                    Title = "Foundation",
                    Description = "Foundation is the first novel in Isaac Asimov's Foundation series. The story follows mathematician Hari Seldon, who predicts the fall of the Galactic Empire. To shorten the impending dark age, he establishes a plan to preserve knowledge and guide humanity's future.",
                    ShortDescription = "A tale of the decline and revival of a galactic empire...",
                    DefaultImageUrl = "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/n/nadace-9788025728802_4.jpg",

                    AuthorId = 7,
                    CategoryId = 3,
                    SeriesId = 7,
                    SeriesOrder = 1,

                    PageCount = 244,
                    DateAdded = DateTime.Now,
                    ReleaseDate = new DateTime(1951, 5, 1),
                    Isbn = "978-0553293357",
                    CopiesInStore = 110,

                    Featured = false,
                    Visible = true,
                    Deleted = false,
                },
                new Book
                {
                    Id = 6,
                    Title = "The Two Towers",
                    Description = "The second part of J.R.R. Tolkien's epic masterpiece The Lord of the Rings. Frodo and the fellowship face new challenges as they continue their quest to destroy the Ring, while war threatens to engulf Middle-earth.",
                    ShortDescription = "The quest continues amidst growing darkness...",
                    DefaultImageUrl = "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/p/pan-prstenu-dve-veze-9788072038305_20.jpg",

                    AuthorId = 4,
                    CategoryId = 2,
                    SeriesId = 4,
                    SeriesOrder = 2,

                    PageCount = 352,
                    DateAdded = DateTime.Now,
                    ReleaseDate = new DateTime(1954, 11, 11),
                    Isbn = "978-0618260262",
                    CopiesInStore = 125,

                    Featured = false,
                    Visible = true,
                    Deleted = false,
                },
                new Book
                {
                    Id = 7,
                    Title = "Foundation and Empire",
                    Description = "Foundation and Empire is the second book in Isaac Asimov's Foundation series. The story follows the rise of a new empire and a confrontation between two powerful forces: the Foundation and the Empire.",
                    ShortDescription = "The struggle for dominance in a galaxy at the edge of chaos...",
                    DefaultImageUrl = "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/n/nadace-a-rise-9788075539182-17.",

                    AuthorId = 7,
                    CategoryId = 3,
                    SeriesId = 7,
                    SeriesOrder = 2,
                    PageCount = 272,

                    DateAdded = DateTime.Now,
                    ReleaseDate = new DateTime(1952, 11, 30),
                    Isbn = "978-0553293371",
                    CopiesInStore = 105,

                    Featured = true,
                    Visible = true,
                    Deleted = false,
                },
                new Book
                {
                    Id = 8,
                    Title = "Murder on the Orient Express",
                    Description = "Hercule Poirot investigates a murder on the luxurious Orient Express train. A classic murder mystery novel by Agatha Christie.",
                    ShortDescription = "A murder mystery on a train...",
                    DefaultImageUrl = "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/v/vrazda-v-orient-expresu-9788024277349_10.jpg",

                    AuthorId = 5,
                    CategoryId = 5,
                    SeriesId = 5,
                    SeriesOrder = 1,

                    PageCount = 256,
                    DateAdded = DateTime.Now,
                    ReleaseDate = new DateTime(1934, 1, 1),
                    Isbn = "978-0062693662",
                    CopiesInStore = 90,

                    Featured = false,
                    Visible = true,
                    Deleted = false,
                },
                new Book
                {
                    Id = 9,
                    Title = "The Stand",
                    Description = "A post-apocalyptic horror novel by Stephen King. After a deadly plague kills most of the world's population, survivors are drawn to two charismatic leaders, leading to a final stand between good and evil.",
                    ShortDescription = "The battle between good and evil after a pandemic...",
                    DefaultImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/The_Stand_%281978%29_front_cover%2C_first_edition.png/220px-The_Stand_%281978%29_front_cover%2C_first_edition.png",

                    AuthorId = 6,
                    CategoryId = 4,
                    SeriesId = 6,
                    SeriesOrder = 1,
                    PageCount = 1153,

                    DateAdded = DateTime.Now,
                    ReleaseDate = new DateTime(1978, 10, 3),
                    Isbn = "978-0307743688",
                    CopiesInStore = 85,

                    Featured = true,
                    Visible = true,
                    Deleted = false,
                },

            };
        }
        public List<BookVariant> SeedBookVariants()
        {
            return new List<BookVariant>
            {
                new BookVariant
                {
                    BookId = 1,
                    BookTypeId = 1,
                    Price = 20.99m,
                    OriginalPrice = 30.99m
                },
                new BookVariant
                {
                    BookId = 1,
                    BookTypeId = 2,
                    Price = 15.99m,
                    OriginalPrice = 20.99m
                },
                new BookVariant
                {
                    BookId = 1,
                    BookTypeId = 4,
                    Price = 10.99m,
                    OriginalPrice = 20.99m
                },
                new BookVariant
                {
                    BookId = 2,
                    BookTypeId = 1,
                    Price = 25.99m,
                    OriginalPrice = 30.99m
                },
                new BookVariant
                {
                    BookId = 2,
                    BookTypeId = 2,
                    Price = 20.99m,
                    OriginalPrice = 25.99m
                },
                new BookVariant
                {
                    BookId = 3,
                    BookTypeId = 1,
                    Price = 30.99m,
                    OriginalPrice = 35.99m
                },
                new BookVariant
                {
                    BookId = 3,
                    BookTypeId = 3,
                    Price = 15.99m,
                    OriginalPrice = 20.99m
                },
                new BookVariant
                {
                    BookId = 4,
                    BookTypeId = 1,
                    Price = 22.99m,
                    OriginalPrice = 28.99m
                },
                new BookVariant
                {
                    BookId = 4,
                    BookTypeId = 3,
                    Price = 17.99m,
                    OriginalPrice = 23.99m
                },
                new BookVariant
                {
                    BookId = 5,
                    BookTypeId = 2,
                    Price = 19.99m,
                    OriginalPrice = 24.99m
                },
                new BookVariant
                {
                    BookId = 5,
                    BookTypeId = 4,
                    Price = 14.99m,
                    OriginalPrice = 19.99m
                },
                new BookVariant
                {
                    BookId = 8,
                    BookTypeId = 1,
                    Price = 14.99m,
                    OriginalPrice = 19.99m
                },
                new BookVariant
                {
                    BookId = 8,
                    BookTypeId = 2,
                    Price = 19.99m,
                    OriginalPrice = 24.99m
                },
                new BookVariant
                {
                    BookId = 9,
                    BookTypeId = 1,
                    Price = 20.99m,
                    OriginalPrice = 25.99m
                },
                new BookVariant
                {
                    BookId = 9,
                    BookTypeId = 3,
                    Price = 15.99m,
                    OriginalPrice = 20.99m
                },
            };

        }
    }
}
