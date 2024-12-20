﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingTour.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookingTour01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserId",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "RefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5fec56cf-3543-4964-85f2-592661715c66", "27872e71-b3cb-4bd5-a88a-3f1d80e05d2c", "User", "USER" },
                    { "f926761e-ba55-4c30-808f-a3c8236246cb", "1fca37ea-3c37-4779-b343-d3fa10b903b4", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "c33ff290-ff40-4cec-9a06-f21b394d0417", 0, "fccf9ee9-434b-4484-8b84-9c4f60c2d4db", "IdentityUser", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEJVUXg/RfT/dIROKEomww41cYGdvO5tzclnM3uNfTTlUixC1XbcTviRsxFrMtPLwew==", null, false, "3472d667-ab25-4ef2-8032-e119ebe4a550", false, "admin@gmail.com" },
                    { "e50b7cfa-c381-4c33-9d48-409d6aa3d948", 0, "6f688c04-50cc-4bf3-bae1-099a699bf506", "IdentityUser", "user@gmail.com", true, false, null, "USER@GMAIL.COM", "USER@GMAIL.COM", "AQAAAAIAAYagAAAAEOlU/rBAGyQqIvFyfiaozMArTsAS7184VbBqQFBS8kSIGrcj0wPiAxtO5lh0hP6poA==", null, false, "45e32a0a-449d-4a75-b693-4fa6f1c18850", false, "user@gmail.com" }
                });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 13, 9, 36, 43, 289, DateTimeKind.Local).AddTicks(5761));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 13, 9, 36, 43, 289, DateTimeKind.Local).AddTicks(5765));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2024, 11, 13, 9, 36, 43, 289, DateTimeKind.Local).AddTicks(5767));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2024, 11, 13, 9, 36, 43, 289, DateTimeKind.Local).AddTicks(5770));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2024, 11, 13, 9, 36, 43, 289, DateTimeKind.Local).AddTicks(5772));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2024, 11, 13, 9, 36, 43, 289, DateTimeKind.Local).AddTicks(5774));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "f926761e-ba55-4c30-808f-a3c8236246cb", "c33ff290-ff40-4cec-9a06-f21b394d0417" },
                    { "5fec56cf-3543-4964-85f2-592661715c66", "e50b7cfa-c381-4c33-9d48-409d6aa3d948" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f926761e-ba55-4c30-808f-a3c8236246cb", "c33ff290-ff40-4cec-9a06-f21b394d0417" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5fec56cf-3543-4964-85f2-592661715c66", "e50b7cfa-c381-4c33-9d48-409d6aa3d948" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5fec56cf-3543-4964-85f2-592661715c66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f926761e-ba55-4c30-808f-a3c8236246cb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c33ff290-ff40-4cec-9a06-f21b394d0417");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e50b7cfa-c381-4c33-9d48-409d6aa3d948");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefreshToken");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 12, 23, 28, 15, 196, DateTimeKind.Local).AddTicks(6816));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 12, 23, 28, 15, 196, DateTimeKind.Local).AddTicks(6822));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2024, 11, 12, 23, 28, 15, 196, DateTimeKind.Local).AddTicks(6826));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2024, 11, 12, 23, 28, 15, 196, DateTimeKind.Local).AddTicks(6830));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2024, 11, 12, 23, 28, 15, 196, DateTimeKind.Local).AddTicks(6834));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2024, 11, 12, 23, 28, 15, 196, DateTimeKind.Local).AddTicks(6844));

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserId",
                table: "RefreshToken",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
