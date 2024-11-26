using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingTour.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixBookingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5826a0c5-af0f-4c57-b49c-386ffce22aa9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce8e8595-5df8-40e4-8606-792a23a0a7e2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1987f970-51b3-42fb-a3dc-0fe4d651aaca");

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 26, 11, 57, 17, 129, DateTimeKind.Local).AddTicks(6536));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 26, 11, 57, 17, 129, DateTimeKind.Local).AddTicks(6540));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2024, 11, 26, 11, 57, 17, 129, DateTimeKind.Local).AddTicks(6544));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2024, 11, 26, 11, 57, 17, 129, DateTimeKind.Local).AddTicks(6547));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2024, 11, 26, 11, 57, 17, 129, DateTimeKind.Local).AddTicks(6550));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2024, 11, 26, 11, 57, 17, 129, DateTimeKind.Local).AddTicks(6554));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Bookings",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5826a0c5-af0f-4c57-b49c-386ffce22aa9", "2", "User", "User" },
                    { "ce8e8595-5df8-40e4-8606-792a23a0a7e2", "1", "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1987f970-51b3-42fb-a3dc-0fe4d651aaca", 0, "7442ac48-9fa9-4f6f-845b-35d6cd7eb44b", "IdentityUser", "user@booking.com", true, true, null, "USER@BOOKING.COM", "USER@BOOKING.COM", "AQAAAAIAAYagAAAAEDpUX0d5qdEgxD7BBfRgPA1xDjYPSKadAug62Qkb6YudznMkYjEtlLZUry9MkJeWSQ==", null, false, "54216e9a-e929-4927-9732-2c0a2581e1fb", false, "user@booking.com" });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "BookingDate", "Notes", "PersonNumber", "PickDate", "StartTime", "Status", "TourId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Customer requested extra seat", 2, new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), 2, 1, "test-user-id" },
                    { 2, new DateTime(2024, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "No special requests", 1, new DateTime(2024, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 14, 30, 0, 0), 1, 2, "test-user-id" }
                });

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
    }
}
