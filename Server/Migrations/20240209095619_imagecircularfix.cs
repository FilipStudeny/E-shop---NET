using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Server.Migrations
{
    /// <inheritdoc />
    public partial class imagecircularfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 1, 26, 12, 35, 7, 289, DateTimeKind.Local).AddTicks(5713));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 1, 26, 12, 35, 7, 289, DateTimeKind.Local).AddTicks(5795));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2024, 1, 26, 12, 35, 7, 289, DateTimeKind.Local).AddTicks(5811));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2024, 1, 26, 12, 35, 7, 289, DateTimeKind.Local).AddTicks(5823));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2024, 1, 26, 12, 35, 7, 289, DateTimeKind.Local).AddTicks(5833));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2024, 1, 26, 12, 35, 7, 289, DateTimeKind.Local).AddTicks(5849));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2024, 1, 26, 12, 35, 7, 289, DateTimeKind.Local).AddTicks(5861));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2024, 1, 26, 12, 35, 7, 289, DateTimeKind.Local).AddTicks(5872));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2024, 1, 26, 12, 35, 7, 289, DateTimeKind.Local).AddTicks(5881));
        }
    }
}
