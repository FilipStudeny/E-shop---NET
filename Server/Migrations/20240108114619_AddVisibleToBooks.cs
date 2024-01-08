using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddVisibleToBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Books",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAdded", "Visible" },
                values: new object[] { new DateTime(2024, 1, 8, 12, 46, 18, 990, DateTimeKind.Local).AddTicks(594), true });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateAdded", "Visible" },
                values: new object[] { new DateTime(2024, 1, 8, 12, 46, 18, 990, DateTimeKind.Local).AddTicks(692), true });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateAdded", "Visible" },
                values: new object[] { new DateTime(2024, 1, 8, 12, 46, 18, 990, DateTimeKind.Local).AddTicks(703), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 1, 8, 12, 37, 18, 74, DateTimeKind.Local).AddTicks(2160));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 1, 8, 12, 37, 18, 74, DateTimeKind.Local).AddTicks(2229));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2024, 1, 8, 12, 37, 18, 74, DateTimeKind.Local).AddTicks(2235));
        }
    }
}
