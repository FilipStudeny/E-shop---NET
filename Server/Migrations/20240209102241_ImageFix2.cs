using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Server.Migrations
{
    /// <inheritdoc />
    public partial class ImageFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 11, 22, 40, 672, DateTimeKind.Local).AddTicks(6675));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 11, 22, 40, 672, DateTimeKind.Local).AddTicks(6748));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 11, 22, 40, 672, DateTimeKind.Local).AddTicks(6757));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 11, 22, 40, 672, DateTimeKind.Local).AddTicks(6765));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 11, 22, 40, 672, DateTimeKind.Local).AddTicks(6771));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 11, 22, 40, 672, DateTimeKind.Local).AddTicks(6782));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 11, 22, 40, 672, DateTimeKind.Local).AddTicks(6790));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 11, 22, 40, 672, DateTimeKind.Local).AddTicks(6798));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 11, 22, 40, 672, DateTimeKind.Local).AddTicks(6805));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 10, 56, 19, 227, DateTimeKind.Local).AddTicks(2442));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 10, 56, 19, 227, DateTimeKind.Local).AddTicks(2528));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 10, 56, 19, 227, DateTimeKind.Local).AddTicks(2540));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 10, 56, 19, 227, DateTimeKind.Local).AddTicks(2550));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 10, 56, 19, 227, DateTimeKind.Local).AddTicks(2558));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 10, 56, 19, 227, DateTimeKind.Local).AddTicks(2572));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 10, 56, 19, 227, DateTimeKind.Local).AddTicks(2582));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 10, 56, 19, 227, DateTimeKind.Local).AddTicks(2590));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2024, 2, 9, 10, 56, 19, 227, DateTimeKind.Local).AddTicks(2599));
        }
    }
}
