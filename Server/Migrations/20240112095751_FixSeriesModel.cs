using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixSeriesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Series",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Series",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 10, 57, 50, 855, DateTimeKind.Local).AddTicks(3167));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 10, 57, 50, 855, DateTimeKind.Local).AddTicks(3256));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 10, 57, 50, 855, DateTimeKind.Local).AddTicks(3270));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 10, 57, 50, 855, DateTimeKind.Local).AddTicks(3282));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 10, 57, 50, 855, DateTimeKind.Local).AddTicks(3293));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 10, 57, 50, 855, DateTimeKind.Local).AddTicks(3310));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 10, 57, 50, 855, DateTimeKind.Local).AddTicks(3322));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 10, 57, 50, 855, DateTimeKind.Local).AddTicks(3335));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2024, 1, 12, 10, 57, 50, 855, DateTimeKind.Local).AddTicks(3344));

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Deleted", "Visible" },
                values: new object[] { false, true });

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Deleted", "Visible" },
                values: new object[] { false, true });

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Deleted", "Visible" },
                values: new object[] { false, true });

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Deleted", "Visible" },
                values: new object[] { false, true });

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Deleted", "Visible" },
                values: new object[] { false, true });

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Deleted", "Visible" },
                values: new object[] { false, true });

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Deleted", "Visible" },
                values: new object[] { false, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Series");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8282));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8353));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8361));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8367));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8374));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8382));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8389));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8396));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2024, 1, 9, 23, 14, 56, 542, DateTimeKind.Local).AddTicks(8402));
        }
    }
}
