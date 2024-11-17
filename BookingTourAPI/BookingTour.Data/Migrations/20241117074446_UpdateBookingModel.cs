using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingTour.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "befc6d9c-9980-46be-b1d3-31af1761c9b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fcd92d17-30f3-42d8-ba39-6c9e84b5e288");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "DateStarts",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "PersonNumber",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PickDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "Bookings",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "914af583-f911-4e17-95ad-6f7827965445", "1", "Admin", "Admin" },
                    { "b31de120-3ff3-4e7d-80eb-5dc000dfc8d8", "2", "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "DateStarts",
                columns: new[] { "DateStartId", "StartDate", "TourId", "TypeStatus" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 11, 5), 2, 4 },
                    { 2, new DateOnly(2024, 11, 10), 2, 4 },
                    { 3, new DateOnly(2024, 11, 15), 2, 4 }
                });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 17, 14, 44, 45, 657, DateTimeKind.Local).AddTicks(7680));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 17, 14, 44, 45, 657, DateTimeKind.Local).AddTicks(7684));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2024, 11, 17, 14, 44, 45, 657, DateTimeKind.Local).AddTicks(7688));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2024, 11, 17, 14, 44, 45, 657, DateTimeKind.Local).AddTicks(7690));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2024, 11, 17, 14, 44, 45, 657, DateTimeKind.Local).AddTicks(7693));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2024, 11, 17, 14, 44, 45, 657, DateTimeKind.Local).AddTicks(7696));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "914af583-f911-4e17-95ad-6f7827965445");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b31de120-3ff3-4e7d-80eb-5dc000dfc8d8");

            migrationBuilder.DeleteData(
                table: "DateStarts",
                keyColumn: "DateStartId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DateStarts",
                keyColumn: "DateStartId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DateStarts",
                keyColumn: "DateStartId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "PersonNumber",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PickDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Bookings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "DateStarts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "befc6d9c-9980-46be-b1d3-31af1761c9b1", "2", "User", "User" },
                    { "fcd92d17-30f3-42d8-ba39-6c9e84b5e288", "1", "Admin", "Admin" }
                });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 16, 23, 49, 10, 973, DateTimeKind.Local).AddTicks(8633));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 16, 23, 49, 10, 973, DateTimeKind.Local).AddTicks(8638));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2024, 11, 16, 23, 49, 10, 973, DateTimeKind.Local).AddTicks(8641));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2024, 11, 16, 23, 49, 10, 973, DateTimeKind.Local).AddTicks(8644));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2024, 11, 16, 23, 49, 10, 973, DateTimeKind.Local).AddTicks(8646));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2024, 11, 16, 23, 49, 10, 973, DateTimeKind.Local).AddTicks(8649));
        }
    }
}
