using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 1, 15, 10, 31, 6, 418, DateTimeKind.Local).AddTicks(1329));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 1, 15, 10, 31, 6, 418, DateTimeKind.Local).AddTicks(1424));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2024, 1, 15, 10, 31, 6, 418, DateTimeKind.Local).AddTicks(1434));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2024, 1, 15, 10, 31, 6, 418, DateTimeKind.Local).AddTicks(1441));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2024, 1, 15, 10, 31, 6, 418, DateTimeKind.Local).AddTicks(1447));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2024, 1, 15, 10, 31, 6, 418, DateTimeKind.Local).AddTicks(1457));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2024, 1, 15, 10, 31, 6, 418, DateTimeKind.Local).AddTicks(1465));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2024, 1, 15, 10, 31, 6, 418, DateTimeKind.Local).AddTicks(1472));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2024, 1, 15, 10, 31, 6, 418, DateTimeKind.Local).AddTicks(1479));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 1, 14, 13, 27, 7, 857, DateTimeKind.Local).AddTicks(1422));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 1, 14, 13, 27, 7, 857, DateTimeKind.Local).AddTicks(1533));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2024, 1, 14, 13, 27, 7, 857, DateTimeKind.Local).AddTicks(1540));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2024, 1, 14, 13, 27, 7, 857, DateTimeKind.Local).AddTicks(1546));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2024, 1, 14, 13, 27, 7, 857, DateTimeKind.Local).AddTicks(1552));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2024, 1, 14, 13, 27, 7, 857, DateTimeKind.Local).AddTicks(1559));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2024, 1, 14, 13, 27, 7, 857, DateTimeKind.Local).AddTicks(1574));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2024, 1, 14, 13, 27, 7, 857, DateTimeKind.Local).AddTicks(1581));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2024, 1, 14, 13, 27, 7, 857, DateTimeKind.Local).AddTicks(1596));
        }
    }
}
