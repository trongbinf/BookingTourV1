using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingTour.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4a4cb8ca-e94e-4951-8466-960e15007f14");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1987f970-51b3-42fb-a3dc-0fe4d651aaca", 0, "7442ac48-9fa9-4f6f-845b-35d6cd7eb44b", "IdentityUser", "user@booking.com", true, true, null, "USER@BOOKING.COM", "USER@BOOKING.COM", "AQAAAAIAAYagAAAAEDpUX0d5qdEgxD7BBfRgPA1xDjYPSKadAug62Qkb6YudznMkYjEtlLZUry9MkJeWSQ==", null, false, "54216e9a-e929-4927-9732-2c0a2581e1fb", false, "user@booking.com" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 25, 9, 40, 2, 728, DateTimeKind.Local).AddTicks(4979));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 25, 9, 40, 2, 728, DateTimeKind.Local).AddTicks(4984));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2024, 11, 25, 9, 40, 2, 728, DateTimeKind.Local).AddTicks(4987));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2024, 11, 25, 9, 40, 2, 728, DateTimeKind.Local).AddTicks(4992));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2024, 11, 25, 9, 40, 2, 728, DateTimeKind.Local).AddTicks(4995));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2024, 11, 25, 9, 40, 2, 728, DateTimeKind.Local).AddTicks(4997));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1987f970-51b3-42fb-a3dc-0fe4d651aaca");

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
    }
}
