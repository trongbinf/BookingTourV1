using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingTour.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDataBookingAndUserDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "914af583-f911-4e17-95ad-6f7827965445");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b31de120-3ff3-4e7d-80eb-5dc000dfc8d8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "69c96014-a2ce-41cd-8b75-ac9f99a5c090", "2", "User", "User" },
                    { "c680712d-6753-431e-8efc-a72d664e69ad", "1", "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "test-user-id", 0, "e6f31d07-8ea1-4f74-a932-2c97e7614a0d", "IdentityUser", "user@booking.com", true, false, null, "USER@BOOKING.COM", "USER@BOOKING.COM", "AQAAAAIAAYagAAAAEBOYZCuylh9ilF4dpMnSNFD2tlNIXrvxcaJ0geB85KhDhPeyAYVC6Zwj8UL8cb7WQg==", null, false, "5ee1f7fb-4343-4730-b74b-0428cff9ccaa", false, "user@booking.com" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 17, 17, 42, 22, 600, DateTimeKind.Local).AddTicks(8424));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 17, 17, 42, 22, 600, DateTimeKind.Local).AddTicks(8431));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2024, 11, 17, 17, 42, 22, 600, DateTimeKind.Local).AddTicks(8439));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2024, 11, 17, 17, 42, 22, 600, DateTimeKind.Local).AddTicks(8446));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2024, 11, 17, 17, 42, 22, 600, DateTimeKind.Local).AddTicks(8454));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2024, 11, 17, 17, 42, 22, 600, DateTimeKind.Local).AddTicks(8462));

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "BookingDate", "Notes", "PersonNumber", "PickDate", "StartTime", "Status", "TourId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Customer requested extra seat", 2, new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), 2, 1, "test-user-id" },
                    { 2, new DateTime(2024, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "No special requests", 1, new DateTime(2024, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 14, 30, 0, 0), 1, 2, "test-user-id" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69c96014-a2ce-41cd-8b75-ac9f99a5c090");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c680712d-6753-431e-8efc-a72d664e69ad");

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "test-user-id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "914af583-f911-4e17-95ad-6f7827965445", "1", "Admin", "Admin" },
                    { "b31de120-3ff3-4e7d-80eb-5dc000dfc8d8", "2", "User", "User" }
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
    }
}
