using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingTour.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "651384b8-391c-4b9d-b473-50f9f9ea5cb3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99caf18e-2954-41d4-ac06-27f34e6c2dc4");

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 1,
                columns: new[] { "ActivityName", "Description", "Duration", "Location" },
                values: new object[] { "Ăn sáng tại khách sạn", "Thưởng thức bữa sáng buffet tại khách sạn trước khi khởi hành.", new TimeSpan(0, 1, 0, 0, 0), "Khách sạn trung tâm Hà Nội" });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 2,
                columns: new[] { "ActivityName", "Description", "Duration", "Location" },
                values: new object[] { "Hướng dẫn tham quan", "Hướng dẫn viên cung cấp thông tin về các địa điểm nổi bật.", new TimeSpan(0, 2, 0, 0, 0), "Điểm tham quan Hà Nội" });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 3,
                columns: new[] { "ActivityName", "Description", "Duration", "Location" },
                values: new object[] { "Tham quan Văn Miếu Quốc Tử Giám", "Khám phá di tích lịch sử với kiến trúc độc đáo.", new TimeSpan(0, 3, 0, 0, 0), "Văn Miếu Quốc Tử Giám" });

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
                columns: new[] { "ActivityName", "ActivityType", "Description", "Duration", "Location" },
                values: new object[] { "Ăn sáng tại khu nghỉ dưỡng", 0, "Bữa sáng với các món ăn đặc sản địa phương.", new TimeSpan(0, 1, 0, 0, 0), "Khu nghỉ dưỡng Đà Lạt" });

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
                columns: new[] { "ActivityName", "Description", "Location", "TourId" },
                values: new object[] { "Đi xe đạp quanh hồ Tuyền Lâm", "Thư giãn với chuyến đi xe đạp quanh hồ.", "Hồ Tuyền Lâm, Đà Lạt", 2 });

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
                    { "b4fe1717-8bcf-4e85-80c7-ecf029aff8e2", "1", "Admin", "Admin" },
                    { "d82ab177-63bb-4206-9347-82ea9f89fef0", "2", "User", "User" }
                });

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 16, 2, 12, 53, 395, DateTimeKind.Local).AddTicks(4086));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 16, 2, 12, 53, 395, DateTimeKind.Local).AddTicks(4091));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2024, 11, 16, 2, 12, 53, 395, DateTimeKind.Local).AddTicks(4097));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2024, 11, 16, 2, 12, 53, 395, DateTimeKind.Local).AddTicks(4102));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2024, 11, 16, 2, 12, 53, 395, DateTimeKind.Local).AddTicks(4105));

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "TourId",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2024, 11, 16, 2, 12, 53, 395, DateTimeKind.Local).AddTicks(4108));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4fe1717-8bcf-4e85-80c7-ecf029aff8e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d82ab177-63bb-4206-9347-82ea9f89fef0");

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 1,
                columns: new[] { "ActivityName", "Description", "Duration", "Location" },
                values: new object[] { "Hướng dẫn viên du lịch", "Cung cấp hướng dẫn viên chuyên nghiệp suốt chuyến tham quan.", new TimeSpan(0, 4, 0, 0, 0), "Thành phố Hà Nội" });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 2,
                columns: new[] { "ActivityName", "Description", "Duration", "Location" },
                values: new object[] { "Quy định khi tham quan", "Du khách không được hút thuốc và giữ trật tự trong khu vực công cộng.", new TimeSpan(0, 1, 0, 0, 0), "Các điểm tham quan" });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 3,
                columns: new[] { "ActivityName", "Description", "Duration", "Location" },
                values: new object[] { "Lịch trình tham quan", "Khám phá các địa danh nổi bật trong thành phố trong 4 giờ.", new TimeSpan(0, 4, 0, 0, 0), "Các địa danh nổi bật tại Hà Nội" });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 4,
                columns: new[] { "ActivityName", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Cắm trại và BBQ", "Trải nghiệm nướng BBQ và nghỉ đêm tại khu cắm trại.", new TimeSpan(1, 0, 0, 0, 0), "Rừng nguyên sinh Đà Lạt", 2 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 5,
                columns: new[] { "ActivityName", "ActivityType", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Bảo vệ môi trường", 1, "Du khách cần tuân thủ các quy định bảo vệ môi trường trong khu cắm trại.", new TimeSpan(0, 0, 30, 0, 0), "Khu cắm trại Đà Lạt", 2 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 6,
                columns: new[] { "ActivityName", "ActivityType", "Description", "Duration", "Location" },
                values: new object[] { "Đi bộ đường dài", 2, "Chuyến đi bộ xuyên rừng kéo dài 3 giờ qua các cảnh quan thiên nhiên.", new TimeSpan(0, 3, 0, 0, 0), "Khu rừng nguyên sinh Đà Lạt" });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 7,
                columns: new[] { "ActivityName", "ActivityType", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Dịch vụ lặn biển", 0, "Hướng dẫn lặn biển chuyên nghiệp và cung cấp trang thiết bị an toàn.", new TimeSpan(0, 2, 0, 0, 0), "Biển Nha Trang", 3 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 8,
                columns: new[] { "ActivityName", "ActivityType", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Quy định trên biển", 1, "Du khách không được vứt rác bừa bãi trên bãi biển.", new TimeSpan(0, 0, 30, 0, 0), "Bãi biển Nha Trang", 3 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 9,
                columns: new[] { "ActivityName", "Description", "Location", "TourId" },
                values: new object[] { "Tham quan san hô", "Lặn biển ngắm san hô và khám phá đáy biển trong 2 giờ.", "Biển Nha Trang", 3 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "ActivityId",
                keyValue: 10,
                columns: new[] { "ActivityName", "ActivityType", "Description", "Duration", "Location", "TourId" },
                values: new object[] { "Hướng dẫn viên bảo tàng", 0, "Hướng dẫn chi tiết về các hiện vật quan trọng trong bảo tàng.", new TimeSpan(0, 3, 0, 0, 0), "Bảo tàng Hà Nội", 4 });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "ActivityId", "ActivityName", "ActivityType", "Description", "Duration", "Location", "TourId" },
                values: new object[,]
                {
                    { 11, "Quy định tại bảo tàng", 1, "Không được chạm vào hiện vật và giữ trật tự trong bảo tàng.", new TimeSpan(0, 0, 30, 0, 0), "Bảo tàng Hà Nội", 4 },
                    { 12, "Khám phá lịch sử", 2, "Tham quan các khu trưng bày lịch sử trong bảo tàng Hà Nội.", new TimeSpan(0, 3, 0, 0, 0), "Bảo tàng Hà Nội", 4 }
                });

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
        }
    }
}
