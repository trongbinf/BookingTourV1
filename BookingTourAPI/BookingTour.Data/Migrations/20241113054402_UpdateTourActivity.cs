using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingTour.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTourActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Tours_TourId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_TourId",
                table: "Activities");

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

            migrationBuilder.CreateTable(
                name: "TourActivities",
                columns: table => new
                {
                    TourId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourActivities", x => new { x.TourId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_TourActivities_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourActivities_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "TourId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 1,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 2,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 3,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 4,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 5,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 6,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 7,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 8,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 9,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 10,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 11,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 12,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 13,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 14,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 15,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 16,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 17,
                column: "TourId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 18,
                column: "TourId",
                value: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "47b6cbac-6b28-46cc-a125-a6962d24ae74", "2", "User", "User" },
                    { "4c97ba02-2d6c-4a95-90bb-84e74b6af561", "1", "Admin", "Admin" }
                });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 13, 12, 43, 59, 239, DateTimeKind.Local).AddTicks(6722));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 13, 12, 43, 59, 239, DateTimeKind.Local).AddTicks(6730));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2024, 11, 13, 12, 43, 59, 239, DateTimeKind.Local).AddTicks(6740));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2024, 11, 13, 12, 43, 59, 239, DateTimeKind.Local).AddTicks(6746));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2024, 11, 13, 12, 43, 59, 239, DateTimeKind.Local).AddTicks(6754));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2024, 11, 13, 12, 43, 59, 239, DateTimeKind.Local).AddTicks(6765));

            migrationBuilder.CreateIndex(
                name: "IX_TourActivities_ActivityId",
                table: "TourActivities",
                column: "ActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourActivities");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47b6cbac-6b28-46cc-a125-a6962d24ae74");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c97ba02-2d6c-4a95-90bb-84e74b6af561");

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 1,
                column: "TourId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 2,
                column: "TourId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 3,
                column: "TourId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 4,
                column: "TourId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 5,
                column: "TourId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 6,
                column: "TourId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 7,
                column: "TourId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 8,
                column: "TourId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 9,
                column: "TourId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 10,
                column: "TourId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 11,
                column: "TourId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 12,
                column: "TourId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 13,
                column: "TourId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 14,
                column: "TourId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 15,
                column: "TourId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 16,
                column: "TourId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 17,
                column: "TourId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 18,
                column: "TourId",
                value: 6);

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

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TourId",
                table: "Activities",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Tours_TourId",
                table: "Activities",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "TourId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
