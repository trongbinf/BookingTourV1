using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingTour.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixDataAndModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourActivities");

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 12);

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

            migrationBuilder.RenameColumn(
                name: "VehicleType",
                table: "Tours",
                newName: "Duration");

            migrationBuilder.AddColumn<int>(
                name: "PersonNumber",
                table: "Tours",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                columns: new[] { "ActivityName", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Ăn sáng tại khách sạn", "Thưởng thức bữa sáng buffet tại khách sạn trước khi khởi hành.", new TimeSpan(0, 1, 0, 0, 0), "Khách sạn trung tâm Hà Nội", 1 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 2,
                columns: new[] { "ActivityName", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Hướng dẫn tham quan", "Hướng dẫn viên cung cấp thông tin về các địa điểm nổi bật.", new TimeSpan(0, 2, 0, 0, 0), "Điểm tham quan Hà Nội", 1 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 3,
                columns: new[] { "ActivityName", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Tham quan Văn Miếu Quốc Tử Giám", "Khám phá di tích lịch sử với kiến trúc độc đáo.", new TimeSpan(0, 3, 0, 0, 0), "Văn Miếu Quốc Tử Giám", 1 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 4,
                columns: new[] { "ActivityName", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Dùng bữa trưa tại nhà hàng", "Thưởng thức ẩm thực truyền thống tại nhà hàng nổi tiếng.", new TimeSpan(0, 1, 30, 0, 0), "Nhà hàng Phố Cổ Hà Nội", 1 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 5,
                columns: new[] { "ActivityName", "ActivityType", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Mua sắm tại chợ Đồng Xuân", 2, "Tự do tham quan và mua sắm các đặc sản địa phương.", new TimeSpan(0, 2, 0, 0, 0), "Chợ Đồng Xuân, Hà Nội", 1 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 6,
                columns: new[] { "ActivityName", "ActivityType", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Ăn sáng tại khu nghỉ dưỡng", 0, "Bữa sáng với các món ăn đặc sản địa phương.", new TimeSpan(0, 1, 0, 0, 0), "Khu nghỉ dưỡng Đà Lạt", 2 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 7,
                columns: new[] { "ActivityName", "ActivityType", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Khám phá thác Datanla", 2, "Tham quan và chụp ảnh tại thác nước nổi tiếng.", new TimeSpan(0, 3, 0, 0, 0), "Thác Datanla, Đà Lạt", 2 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 8,
                columns: new[] { "ActivityName", "ActivityType", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Dùng bữa trưa ngoài trời", 0, "Bữa trưa BBQ tại khu vực gần thác nước.", new TimeSpan(0, 2, 0, 0, 0), "Khu vực cắm trại, Đà Lạt", 2 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 9,
                columns: new[] { "ActivityName", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Đi xe đạp quanh hồ Tuyền Lâm", "Thư giãn với chuyến đi xe đạp quanh hồ.", new TimeSpan(0, 2, 0, 0, 0), "Hồ Tuyền Lâm, Đà Lạt", 2 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 10,
                columns: new[] { "ActivityName", "ActivityType", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Hướng dẫn bảo vệ môi trường", 1, "Thực hiện các quy định bảo vệ môi trường khu tham quan.", new TimeSpan(0, 0, 30, 0, 0), "Khu vực thiên nhiên, Đà Lạt", 2 });

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
                columns: new[] { "Created", "Duration", "PersonNumber" },
                values: new object[] { new DateTime(2024, 11, 16, 23, 49, 10, 973, DateTimeKind.Local).AddTicks(8633), "1 ngày 1 đêm", 30 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 2,
                columns: new[] { "Created", "Duration", "PersonNumber" },
                values: new object[] { new DateTime(2024, 11, 16, 23, 49, 10, 973, DateTimeKind.Local).AddTicks(8638), "2 ngày 1 đêm", 16 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 3,
                columns: new[] { "Created", "Duration", "PersonNumber" },
                values: new object[] { new DateTime(2024, 11, 16, 23, 49, 10, 973, DateTimeKind.Local).AddTicks(8641), "3 ngày 2 đêm", 19 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 4,
                columns: new[] { "Created", "Duration", "PersonNumber" },
                values: new object[] { new DateTime(2024, 11, 16, 23, 49, 10, 973, DateTimeKind.Local).AddTicks(8644), "1 ngày 1 đêm", 19 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 5,
                columns: new[] { "Created", "Duration", "PersonNumber" },
                values: new object[] { new DateTime(2024, 11, 16, 23, 49, 10, 973, DateTimeKind.Local).AddTicks(8646), "2 ngày 2 đêm", 15 });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 6,
                columns: new[] { "Created", "Duration", "PersonNumber" },
                values: new object[] { new DateTime(2024, 11, 16, 23, 49, 10, 973, DateTimeKind.Local).AddTicks(8649), "4 ngày 3 đêm", 26 });

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
                keyValue: "befc6d9c-9980-46be-b1d3-31af1761c9b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fcd92d17-30f3-42d8-ba39-6c9e84b5e288");

            migrationBuilder.DropColumn(
                name: "PersonNumber",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Tours",
                newName: "VehicleType");

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
                columns: new[] { "ActivityName", "Description", "TourId" },
                values: new object[] { "Hướng dẫn viên du lịch", "Cung cấp hướng dẫn viên chuyên nghiệp suốt chuyến tham quan.", 0 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 2,
                columns: new[] { "ActivityName", "Description", "TourId" },
                values: new object[] { "Quy định khi tham quan", "Du khách không được hút thuốc và giữ trật tự trong khu vực công cộng.", 0 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 3,
                columns: new[] { "ActivityName", "Description", "TourId" },
                values: new object[] { "Lịch trình tham quan", "Khám phá các địa danh nổi bật trong thành phố trong 4 giờ.", 0 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 4,
                columns: new[] { "ActivityName", "Description", "TourId" },
                values: new object[] { "Cắm trại và BBQ", "Trải nghiệm nướng BBQ và nghỉ đêm tại khu cắm trại.", 0 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 5,
                columns: new[] { "ActivityName", "ActivityType", "Description", "TourId" },
                values: new object[] { "Bảo vệ môi trường", 1, "Du khách cần tuân thủ các quy định bảo vệ môi trường trong khu cắm trại.", 0 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 6,
                columns: new[] { "ActivityName", "ActivityType", "Description", "TourId" },
                values: new object[] { "Đi bộ đường dài", 2, "Chuyến đi bộ xuyên rừng kéo dài 3 giờ qua các cảnh quan thiên nhiên.", 0 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 7,
                columns: new[] { "ActivityName", "ActivityType", "Description", "TourId" },
                values: new object[] { "Dịch vụ lặn biển", 0, "Hướng dẫn lặn biển chuyên nghiệp và cung cấp trang thiết bị an toàn.", 0 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 8,
                columns: new[] { "ActivityName", "ActivityType", "Description", "TourId" },
                values: new object[] { "Quy định trên biển", 1, "Du khách không được vứt rác bừa bãi trên bãi biển.", 0 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 9,
                columns: new[] { "ActivityName", "Description", "TourId" },
                values: new object[] { "Tham quan san hô", "Lặn biển ngắm san hô và khám phá đáy biển trong 2 giờ.", 0 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 10,
                columns: new[] { "ActivityName", "ActivityType", "Description", "TourId" },
                values: new object[] { "Hướng dẫn viên bảo tàng", 0, "Hướng dẫn chi tiết về các hiện vật quan trọng trong bảo tàng.", 0 });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "ActivityId", "ActivityName", "ActivityType", "Description", "TourId" },
                values: new object[,]
                {
                    { 11, "Quy định tại bảo tàng", 1, "Không được chạm vào hiện vật và giữ trật tự trong bảo tàng.", 0 },
                    { 12, "Lịch trình tham quan bảo tàng", 2, "Khám phá bảo tàng trong khoảng thời gian 3 giờ.", 0 },
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
                columns: new[] { "Created", "VehicleType" },
                values: new object[] { new DateTime(2024, 11, 13, 12, 43, 59, 239, DateTimeKind.Local).AddTicks(6722), "Xe bus" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 2,
                columns: new[] { "Created", "VehicleType" },
                values: new object[] { new DateTime(2024, 11, 13, 12, 43, 59, 239, DateTimeKind.Local).AddTicks(6730), "Xe du lịch" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 3,
                columns: new[] { "Created", "VehicleType" },
                values: new object[] { new DateTime(2024, 11, 13, 12, 43, 59, 239, DateTimeKind.Local).AddTicks(6740), "Xe riêng" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 4,
                columns: new[] { "Created", "VehicleType" },
                values: new object[] { new DateTime(2024, 11, 13, 12, 43, 59, 239, DateTimeKind.Local).AddTicks(6746), "Xe điện" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 5,
                columns: new[] { "Created", "VehicleType" },
                values: new object[] { new DateTime(2024, 11, 13, 12, 43, 59, 239, DateTimeKind.Local).AddTicks(6754), "Xe du lịch" });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 6,
                columns: new[] { "Created", "VehicleType" },
                values: new object[] { new DateTime(2024, 11, 13, 12, 43, 59, 239, DateTimeKind.Local).AddTicks(6765), "Thuyền" });

            migrationBuilder.CreateIndex(
                name: "IX_TourActivities_ActivityId",
                table: "TourActivities",
                column: "ActivityId");
        }
    }
}
