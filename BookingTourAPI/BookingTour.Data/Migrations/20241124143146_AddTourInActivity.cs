using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingTour.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTourInActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "test-user-id");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4a4cb8ca-e94e-4951-8466-960e15007f14", 0, "8753e06c-00b9-44fe-9bb2-6ac3aa06ae87", "IdentityUser", "user@booking.com", true, true, null, "USER@BOOKING.COM", "USER@BOOKING.COM", "AQAAAAIAAYagAAAAEHfyM3wS/qquViiWG7lf2P5iyuXw/YEkV4DpO/sVgUy6LNuNDjkQxb1WLEv8nyZHbA==", null, false, "f5daa429-53bb-4a48-9770-fdcb053e0f1e", false, "user@booking.com" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 24, 21, 31, 39, 929, DateTimeKind.Local).AddTicks(7860));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 24, 21, 31, 39, 929, DateTimeKind.Local).AddTicks(7870));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2024, 11, 24, 21, 31, 39, 929, DateTimeKind.Local).AddTicks(7875));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2024, 11, 24, 21, 31, 39, 929, DateTimeKind.Local).AddTicks(7884));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2024, 11, 24, 21, 31, 39, 929, DateTimeKind.Local).AddTicks(7888));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2024, 11, 24, 21, 31, 39, 929, DateTimeKind.Local).AddTicks(7895));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4a4cb8ca-e94e-4951-8466-960e15007f14");

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
        }
    }
}
