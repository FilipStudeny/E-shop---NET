using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Eshop.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedinInitialProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Image", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Lorem ipsum sudo lomen sili imeni anto destre umeni quarto luremo lumeni", "https://dummyimage.com/600x400/ffffff/000000", 9.99m, "Book 1" },
                    { 2, "Lorem ipsum sudo lomen sili imeni anto destre umeni quarto luremo lumeni", "https://dummyimage.com/600x400/ffffff/000000", 9.99m, "Book 2" },
                    { 3, "Lorem ipsum sudo lomen sili imeni anto destre umeni quarto luremo lumeni", "https://dummyimage.com/600x400/ffffff/000000", 9.99m, "Book 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
