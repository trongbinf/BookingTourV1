using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingTour.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixActivityColum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourActivities");

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47b6cbac-6b28-46cc-a125-a6962d24ae74");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c97ba02-2d6c-4a95-90bb-84e74b6af561");

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

            migrationBuilder.DeleteData(
                table: "DateStarts",
                keyColumn: "DateStartId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DateStarts",
                keyColumn: "DateStartId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DateStarts",
                keyColumn: "DateStartId",
                keyValue: 6);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Activities",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 1,
                columns: new[] { "Duration", "Location", "TourId" },
                values: new object[] { new TimeSpan(0, 4, 0, 0, 0), "Thành phố Hà Nội", 1 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 2,
                columns: new[] { "Duration", "Location", "TourId" },
                values: new object[] { new TimeSpan(0, 1, 0, 0, 0), "Các điểm tham quan", 1 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 3,
                columns: new[] { "Duration", "Location", "TourId" },
                values: new object[] { new TimeSpan(0, 4, 0, 0, 0), "Các địa danh nổi bật tại Hà Nội", 1 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 4,
                columns: new[] { "Duration", "Location", "TourId" },
                values: new object[] { new TimeSpan(1, 0, 0, 0, 0), "Rừng nguyên sinh Đà Lạt", 2 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 5,
                columns: new[] { "Duration", "Location", "TourId" },
                values: new object[] { new TimeSpan(0, 0, 30, 0, 0), "Khu cắm trại Đà Lạt", 2 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 6,
                columns: new[] { "Duration", "Location", "TourId" },
                values: new object[] { new TimeSpan(0, 3, 0, 0, 0), "Khu rừng nguyên sinh Đà Lạt", 2 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 7,
                columns: new[] { "Duration", "Location", "TourId" },
                values: new object[] { new TimeSpan(0, 2, 0, 0, 0), "Biển Nha Trang", 3 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 8,
                columns: new[] { "Duration", "Location", "TourId" },
                values: new object[] { new TimeSpan(0, 0, 30, 0, 0), "Bãi biển Nha Trang", 3 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 9,
                columns: new[] { "Duration", "Location", "TourId" },
                values: new object[] { new TimeSpan(0, 2, 0, 0, 0), "Biển Nha Trang", 3 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 10,
                columns: new[] { "Duration", "Location", "TourId" },
                values: new object[] { new TimeSpan(0, 3, 0, 0, 0), "Bảo tàng Hà Nội", 4 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 11,
                columns: new[] { "Duration", "Location", "TourId" },
                values: new object[] { new TimeSpan(0, 0, 30, 0, 0), "Bảo tàng Hà Nội", 4 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 12,
                columns: new[] { "ActivityName", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Khám phá lịch sử", "Tham quan các khu trưng bày lịch sử trong bảo tàng Hà Nội.", new TimeSpan(0, 3, 0, 0, 0), "Bảo tàng Hà Nội", 4 });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "651384b8-391c-4b9d-b473-50f9f9ea5cb3", "1", "Admin", "Admin" },
                    { "99caf18e-2954-41d4-ac06-27f34e6c2dc4", "2", "User", "User" }
                });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 16, 2, 1, 38, 861, DateTimeKind.Local).AddTicks(70));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 16, 2, 1, 38, 861, DateTimeKind.Local).AddTicks(75));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2024, 11, 16, 2, 1, 38, 861, DateTimeKind.Local).AddTicks(78));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2024, 11, 16, 2, 1, 38, 861, DateTimeKind.Local).AddTicks(81));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2024, 11, 16, 2, 1, 38, 861, DateTimeKind.Local).AddTicks(84));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2024, 11, 16, 2, 1, 38, 861, DateTimeKind.Local).AddTicks(86));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Tours_TourId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_TourId",
                table: "Activities");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "651384b8-391c-4b9d-b473-50f9f9ea5cb3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99caf18e-2954-41d4-ac06-27f34e6c2dc4");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Activities");

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
                columns: new[] { "ActivityName", "Description", "TourId" },
                values: new object[] { "Lịch trình tham quan bảo tàng", "Khám phá bảo tàng trong khoảng thời gian 3 giờ.", 0 });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "ActivityId", "ActivityName", "ActivityType", "Description", "TourId" },
                values: new object[,]
                {
                    { 13, "Thuê lều và dụng cụ", 0, "Cung cấp các dịch vụ thuê lều và dụng cụ cắm trại đầy đủ.", 0 },
                    { 14, "Quy định cắm trại", 1, "Không gây tiếng ồn sau 10 giờ tối và tuân thủ các quy định an toàn.", 0 },
                    { 15, "Tham quan rừng", 2, "Đi bộ ngắm cảnh rừng và nghỉ qua đêm tại khu vực cắm trại.", 0 },
                    { 16, "Dịch vụ đưa đón", 0, "Dịch vụ xe đưa đón từ sân bay và các điểm đón đến khu resort.", 0 },
                    { 17, "Quy định an toàn", 1, "Du khách không được mang theo các vật dụng nguy hiểm vào khu nghỉ dưỡng.", 0 },
                    { 18, "Nghỉ dưỡng tại resort", 2, "Trải nghiệm nghỉ dưỡng và tham gia các hoạt động giải trí tại resort.", 0 }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "47b6cbac-6b28-46cc-a125-a6962d24ae74", "2", "User", "User" },
                    { "4c97ba02-2d6c-4a95-90bb-84e74b6af561", "1", "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "DateStarts",
                columns: new[] { "DateStartId", "StartDate", "TourId", "TypeStatus" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 0 },
                    { 2, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 0 },
                    { 3, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 0 },
                    { 4, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 0 },
                    { 5, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 0 },
                    { 6, new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 0 }
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
    }
}
