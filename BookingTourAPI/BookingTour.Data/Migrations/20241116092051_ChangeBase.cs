using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingTour.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c6262a4-f3ea-45c7-abf4-ce43582886e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb9e40d0-5e41-4ecc-859f-a10f5dc000ba");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "dd90a05f-d51d-4dd6-bac2-475b023437f5", "1", "Admin", "Admin" },
                    { "e2ae9aeb-9999-412a-9948-9bdd9381c3d3", "2", "User", "User" }
                });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 16, 16, 20, 47, 471, DateTimeKind.Local).AddTicks(5953));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 16, 16, 20, 47, 471, DateTimeKind.Local).AddTicks(5957));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2024, 11, 16, 16, 20, 47, 471, DateTimeKind.Local).AddTicks(5960));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2024, 11, 16, 16, 20, 47, 471, DateTimeKind.Local).AddTicks(5962));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2024, 11, 16, 16, 20, 47, 471, DateTimeKind.Local).AddTicks(5965));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2024, 11, 16, 16, 20, 47, 471, DateTimeKind.Local).AddTicks(5968));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd90a05f-d51d-4dd6-bac2-475b023437f5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2ae9aeb-9999-412a-9948-9bdd9381c3d3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6c6262a4-f3ea-45c7-abf4-ce43582886e1", "2", "User", "User" },
                    { "eb9e40d0-5e41-4ecc-859f-a10f5dc000ba", "1", "Admin", "Admin" }
                });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 16, 14, 31, 29, 324, DateTimeKind.Local).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 16, 14, 31, 29, 324, DateTimeKind.Local).AddTicks(1664));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2024, 11, 16, 14, 31, 29, 324, DateTimeKind.Local).AddTicks(1667));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2024, 11, 16, 14, 31, 29, 324, DateTimeKind.Local).AddTicks(1670));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2024, 11, 16, 14, 31, 29, 324, DateTimeKind.Local).AddTicks(1673));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2024, 11, 16, 14, 31, 29, 324, DateTimeKind.Local).AddTicks(1676));
        }
    }
}
