using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitModelsAndSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BiographyText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BookTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTypes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShortDescription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefaultImageUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SeriesId = table.Column<int>(type: "int", nullable: false),
                    SeriesOrder = table.Column<int>(type: "int", nullable: false),
                    PageCount = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Isbn = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Featured = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CopiesInStore = table.Column<int>(type: "int", nullable: false),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BookVariants",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    BookTypeId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookVariants", x => new { x.BookId, x.BookTypeId });
                    table.ForeignKey(
                        name: "FK_BookVariants_BookTypes_BookTypeId",
                        column: x => x.BookTypeId,
                        principalTable: "BookTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookVariants_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BookId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BiographyText", "Image", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "No author", "", "No author", "no-author" },
                    { 2, "Joanne Rowling, better known by her pen name J.K. Rowling, is a British author, best known for writing the Harry Potter series.", "", "J.K. Rowling", "joanne-rowling" },
                    { 3, "George Raymond Richard Martin, often referred to as GRRM, is an American novelist and short story writer, known for his series A Song of Ice and Fire.", "", "George R.R. Martin", "george-raymond-richard-martin" },
                    { 4, "John Ronald Reuel Tolkien was an English writer, poet, and university professor. He is best known as the author of the classic high-fantasy works The Hobbit, The Lord of the Rings, and The Silmarillion.", "", "J.R.R. Tolkien", "john-ronald-reuel-tolkien" },
                    { 5, "Agatha Christie was an English writer known for her detective novels, particularly those revolving around her fictional detectives Hercule Poirot and Miss Marple.", "", "Agatha Christie", "agatha-christie" },
                    { 6, "Stephen King is an American author of horror, supernatural fiction, suspense, crime, science-fiction, and fantasy novels.", "", "Stephen King", "stephen-king" },
                    { 7, "Renowned science fiction writer", "", "Isaac Asimov", "issac-asimov" }
                });

            migrationBuilder.InsertData(
                table: "BookTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Paperback" },
                    { 2, "Hardcover" },
                    { 3, "Ebook" },
                    { 4, "Audiobook" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Deleted", "Name", "Url", "Visible" },
                values: new object[,]
                {
                    { 1, false, "Default", "default", true },
                    { 2, false, "Fantasy", "fantasy", true },
                    { 3, false, "Sci-fi", "scifi", true },
                    { 4, false, "Horror", "horror", true },
                    { 5, false, "Detective", "detective", true }
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "Description", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Books that are not part of any series", "Stand alone", "stand-alone" },
                    { 2, "The magical adventures of Harry Potter", "Harry Potter", "harry-potter" },
                    { 3, "Epic fantasy series by George R.R. Martin", "A Song of Ice and Fire", "a-song-of-ice-and-fire" },
                    { 4, "Classic high-fantasy series by J.R.R. Tolkien", "The Lord of the Rings", "the-lord-of-the-rings" },
                    { 5, "Detective series by Agatha Christie", "Hercule Poirot", "hercule-poirot" },
                    { 6, "Horror novels by Stephen King", "Stephen King Horror Novels", "stephen-king-horror-novels" },
                    { 7, "Sci-fi series by Isaac Asimov", "Foundation", "foundation" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CopiesInStore", "DateAdded", "DefaultImageUrl", "Deleted", "Description", "Featured", "Isbn", "PageCount", "ReleaseDate", "SeriesId", "SeriesOrder", "ShortDescription", "Title", "Visible" },
                values: new object[,]
                {
                    { 1, 3, 2, 150, new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8282), "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/h/hra-o-truny-9788025722824.jpg", false, "Winter is coming. A classic world that best describes George R. R. Martin's epic saga A Song of Ice and Fire. The kingdoms of Westeros have a long summer, which must be followed by a harsh winter. But before that, and before the Others return, the game of thrones is being played out, and there are far fewer contenders than contenders. Perhaps the only one who doesn't want the Iron Throne is Lord Eddard Stark, guardian of the north. Therefore, he must become the mover of events that will change everything.", true, "978-80-257-2282-4", 694, new DateTime(1996, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1, " The kingdoms of Westeros have a long summer, which must be followed by a harsh winter. But before that, and before the Others return, the game of thrones is being played out, and there are far fewer contenders than contenders.", "Game of Thrones", true },
                    { 2, 3, 2, 120, new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8353), "https://www.knihydobrovsky.cz/kniha/stret-kralu-39500927", false, "The Seven Kingdoms are divided by revolt and blood feud, and winter approaches like an angry beast. Beyond the Wall, the armies of the Night's Watch are massing for an assault on the kingdom of the dead. The young, the brave, and the foolish, all compete for the throne.", false, "978-0553579901", 768, new DateTime(1998, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, "Winter approaches and armies gather. The battle for the throne continues...", "A Clash of Kings", true },
                    { 3, 3, 2, 100, new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8361), "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/b/boure-mecu-9788025724187_17.jpg", false, "The Seven Kingdoms are divided by revolt and blood feud, and winter approaches like an angry beast. Beyond the Wall, the armies of the Night's Watch are massing for an assault on the kingdom of the dead. The young, the brave, and the foolish, all compete for the throne.", true, "978-0553573428", 992, new DateTime(2000, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, "Winter approaches and armies gather. The battle for the throne continues...", "A Storm of Swords", true },
                    { 4, 4, 2, 130, new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8367), "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/p/pan-prstenu-spolecenstvo-prstenu-ilustrovane-vydani-9788072038299_26.jpg", false, "The first part of J.R.R. Tolkien's epic masterpiece The Lord of the Rings. The story begins with the hobbit Frodo Baggins inheriting the Ring from his uncle Bilbo. Together with his fellowship, he embarks on a dangerous journey to destroy the Ring and defeat the Dark Lord Sauron.", false, "978-0618640157", 432, new DateTime(1954, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, "The journey to destroy the One Ring begins...", "The Fellowship of the Ring", true },
                    { 5, 7, 3, 110, new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8374), "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/n/nadace-9788025728802_4.jpg", false, "Foundation is the first novel in Isaac Asimov's Foundation series. The story follows mathematician Hari Seldon, who predicts the fall of the Galactic Empire. To shorten the impending dark age, he establishes a plan to preserve knowledge and guide humanity's future.", false, "978-0553293357", 244, new DateTime(1951, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 1, "A tale of the decline and revival of a galactic empire...", "Foundation", true },
                    { 6, 4, 2, 125, new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8382), "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/p/pan-prstenu-dve-veze-9788072038305_20.jpg", false, "The second part of J.R.R. Tolkien's epic masterpiece The Lord of the Rings. Frodo and the fellowship face new challenges as they continue their quest to destroy the Ring, while war threatens to engulf Middle-earth.", false, "978-0618260262", 352, new DateTime(1954, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "The quest continues amidst growing darkness...", "The Two Towers", true },
                    { 7, 7, 3, 105, new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8389), "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/n/nadace-a-rise-9788075539182-17.", false, "Foundation and Empire is the second book in Isaac Asimov's Foundation series. The story follows the rise of a new empire and a confrontation between two powerful forces: the Foundation and the Empire.", true, "978-0553293371", 272, new DateTime(1952, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 2, "The struggle for dominance in a galaxy at the edge of chaos...", "Foundation and Empire", true },
                    { 8, 5, 5, 90, new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8396), "https://www.knihydobrovsky.cz/thumbs/book-detail-fancy-box/mod_eshop/produkty/v/vrazda-v-orient-expresu-9788024277349_10.jpg", false, "Hercule Poirot investigates a murder on the luxurious Orient Express train. A classic murder mystery novel by Agatha Christie.", false, "978-0062693662", 256, new DateTime(1934, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1, "A murder mystery on a train...", "Murder on the Orient Express", true },
                    { 9, 6, 4, 85, new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8402), "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/The_Stand_%281978%29_front_cover%2C_first_edition.png/220px-The_Stand_%281978%29_front_cover%2C_first_edition.png", false, "A post-apocalyptic horror novel by Stephen King. After a deadly plague kills most of the world's population, survivors are drawn to two charismatic leaders, leading to a final stand between good and evil.", true, "978-0307743688", 1153, new DateTime(1978, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1, "The battle between good and evil after a pandemic...", "The Stand", true }
                });

            migrationBuilder.InsertData(
                table: "BookVariants",
                columns: new[] { "BookId", "BookTypeId", "Deleted", "OriginalPrice", "Price", "Visible" },
                values: new object[,]
                {
                    { 1, 1, false, 30.99m, 20.99m, true },
                    { 1, 2, false, 20.99m, 15.99m, true },
                    { 1, 4, false, 20.99m, 10.99m, true },
                    { 2, 1, false, 30.99m, 25.99m, true },
                    { 2, 2, false, 25.99m, 20.99m, true },
                    { 3, 1, false, 35.99m, 30.99m, true },
                    { 3, 3, false, 20.99m, 15.99m, true },
                    { 4, 1, false, 28.99m, 22.99m, true },
                    { 4, 3, false, 23.99m, 17.99m, true },
                    { 5, 2, false, 24.99m, 19.99m, true },
                    { 5, 4, false, 19.99m, 14.99m, true },
                    { 8, 1, false, 19.99m, 14.99m, true },
                    { 8, 2, false, 24.99m, 19.99m, true },
                    { 9, 1, false, 25.99m, 20.99m, true },
                    { 9, 3, false, 20.99m, 15.99m, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_SeriesId",
                table: "Books",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_BookVariants_BookTypeId",
                table: "BookVariants",
                column: "BookTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_BookId",
                table: "Images",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookVariants");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "BookTypes");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Series");
        }
    }
}
