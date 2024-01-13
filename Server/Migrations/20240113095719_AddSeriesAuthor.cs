using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddSeriesAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Series",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 1, 13, 10, 57, 19, 268, DateTimeKind.Local).AddTicks(2393));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 1, 13, 10, 57, 19, 268, DateTimeKind.Local).AddTicks(2464));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2024, 1, 13, 10, 57, 19, 268, DateTimeKind.Local).AddTicks(2471));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2024, 1, 13, 10, 57, 19, 268, DateTimeKind.Local).AddTicks(2477));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2024, 1, 13, 10, 57, 19, 268, DateTimeKind.Local).AddTicks(2484));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2024, 1, 13, 10, 57, 19, 268, DateTimeKind.Local).AddTicks(2493));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2024, 1, 13, 10, 57, 19, 268, DateTimeKind.Local).AddTicks(2500));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2024, 1, 13, 10, 57, 19, 268, DateTimeKind.Local).AddTicks(2506));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2024, 1, 13, 10, 57, 19, 268, DateTimeKind.Local).AddTicks(2513));

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 1,
                column: "AuthorId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 2,
                column: "AuthorId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 3,
                column: "AuthorId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 4,
                column: "AuthorId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 5,
                column: "AuthorId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 6,
                column: "AuthorId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 7,
                column: "AuthorId",
                value: 7);

            migrationBuilder.CreateIndex(
                name: "IX_Series_AuthorId",
                table: "Series",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Authors_AuthorId",
                table: "Series",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Series_Authors_AuthorId",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Series_AuthorId",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Series");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 18, 17, 39, 17, DateTimeKind.Local).AddTicks(229));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 18, 17, 39, 17, DateTimeKind.Local).AddTicks(298));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 18, 17, 39, 17, DateTimeKind.Local).AddTicks(306));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 18, 17, 39, 17, DateTimeKind.Local).AddTicks(314));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 18, 17, 39, 17, DateTimeKind.Local).AddTicks(320));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 18, 17, 39, 17, DateTimeKind.Local).AddTicks(329));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 18, 17, 39, 17, DateTimeKind.Local).AddTicks(337));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 18, 17, 39, 17, DateTimeKind.Local).AddTicks(343));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 18, 17, 39, 17, DateTimeKind.Local).AddTicks(350));
        }
    }
}
