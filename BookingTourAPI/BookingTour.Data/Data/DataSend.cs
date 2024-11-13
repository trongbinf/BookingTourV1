using BookingTour.Model;
using BookingTour.Model.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookingTour.Data.Data
{
    public class DataSend
    {
        public static void InsertData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData
              (
              new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
              new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
              );

            // Thêm dữ liệu cho bảng Category
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Tour tham quan", Status = true },
                new Category { CategoryId = 2, Name = "Thiên nhiên", Status = true },
                new Category { CategoryId = 3, Name = "Biển", Status = true }
            );

            // Thêm dữ liệu cho bảng Tour và liên kết với Category thông qua CategoryId
            modelBuilder.Entity<Tour>().HasData(
                new Tour
                {
                    TourId = 1,
                    TourName = "Tham quan thành phố",
                    Description = "Tour tham quan các danh lam thắng cảnh trong thành phố.",
                    City = "Hà Nội",
                    Country = "Việt Nam",
                    IsFullDay = true,
                    Price = 500000,
                    VehicleType = "Xe bus",
                    Status = true,
                    Created = DateTime.Now,
                    CategoryId = 1  // Thuộc Category "Tour tham quan"
                },
                new Tour
                {
                    TourId = 2,
                    TourName = "Khám phá thiên nhiên",
                    Description = "Tour khám phá các khu rừng nguyên sinh.",
                    City = "Đà Lạt",
                    Country = "Việt Nam",
                    IsFullDay = false,
                    Price = 800000,
                    VehicleType = "Xe du lịch",
                    Status = true,
                    Created = DateTime.Now,
                    CategoryId = 2  // Thuộc Category "Thiên nhiên"
                },
                new Tour
                {
                    TourId = 3,
                    TourName = "Du lịch biển",
                    Description = "Tour du lịch nghỉ dưỡng tại các bãi biển đẹp.",
                    City = "Nha Trang",
                    Country = "Việt Nam",
                    IsFullDay = false,
                    Price = 1200000,
                    VehicleType = "Xe riêng",
                    Status = true,
                    Created = DateTime.Now,
                    CategoryId = 3  // Thuộc Category "Biển"
                },
                new Tour
                {
                    TourId = 4,
                    TourName = "Tham quan bảo tàng",
                    Description = "Tham quan các bảo tàng nổi tiếng.",
                    City = "Hà Nội",
                    Country = "Việt Nam",
                    IsFullDay = true,
                    Price = 300000,
                    VehicleType = "Xe điện",
                    Status = true,
                    Created = DateTime.Now,
                    CategoryId = 1  // Thuộc Category "Tour tham quan"
                },
                new Tour
                {
                    TourId = 5,
                    TourName = "Cắm trại rừng",
                    Description = "Tour cắm trại qua đêm trong rừng.",
                    City = "Sa Pa",
                    Country = "Việt Nam",
                    IsFullDay = false,
                    Price = 950000,
                    VehicleType = "Xe du lịch",
                    Status = true,
                    Created = DateTime.Now,
                    CategoryId = 2  // Thuộc Category "Thiên nhiên"
                },
                new Tour
                {
                    TourId = 6,
                    TourName = "Kỳ nghỉ biển",
                    Description = "Tour nghỉ dưỡng và tham quan vùng biển.",
                    City = "Phú Quốc",
                    Country = "Việt Nam",
                    IsFullDay = true,
                    Price = 1500000,
                    VehicleType = "Thuyền",
                    Status = true,
                    Created = DateTime.Now,
                    CategoryId = 3  // Thuộc Category "Biển"
                }
            );

            // Thêm dữ liệu cho bảng Activity và liên kết với các tour
            modelBuilder.Entity<Activity>().HasData(
                // Hoạt động cho tour "Tham quan thành phố"
                new Activity
                {
                    ActivityId = 1,
                    ActivityType = ActivityType.Services,
                    ActivityName = "Hướng dẫn viên du lịch",
                    Description = "Cung cấp hướng dẫn viên chuyên nghiệp suốt chuyến tham quan.",
                },
                new Activity
                {
                    ActivityId = 2,
                    ActivityType = ActivityType.Rules,
                    ActivityName = "Quy định khi tham quan",
                    Description = "Du khách không được hút thuốc và giữ trật tự trong khu vực công cộng.",
                },
                new Activity
                {
                    ActivityId = 3,
                    ActivityType = ActivityType.Schedule,
                    ActivityName = "Lịch trình tham quan",
                    Description = "Khám phá các địa danh nổi bật trong thành phố trong 4 giờ.",
                },

                // Hoạt động cho tour "Khám phá thiên nhiên"
                new Activity
                {
                    ActivityId = 4,
                    ActivityType = ActivityType.Services,
                    ActivityName = "Cắm trại và BBQ",
                    Description = "Trải nghiệm nướng BBQ và nghỉ đêm tại khu cắm trại.",
                },
                new Activity
                {
                    ActivityId = 5,
                    ActivityType = ActivityType.Rules,
                    ActivityName = "Bảo vệ môi trường",
                    Description = "Du khách cần tuân thủ các quy định bảo vệ môi trường trong khu cắm trại.",
                },
                new Activity
                {
                    ActivityId = 6,
                    ActivityType = ActivityType.Schedule,
                    ActivityName = "Đi bộ đường dài",
                    Description = "Chuyến đi bộ xuyên rừng kéo dài 3 giờ qua các cảnh quan thiên nhiên.",
                },

                // Hoạt động cho tour "Du lịch biển"
                new Activity
                {
                    ActivityId = 7,
                    ActivityType = ActivityType.Services,
                    ActivityName = "Dịch vụ lặn biển",
                    Description = "Hướng dẫn lặn biển chuyên nghiệp và cung cấp trang thiết bị an toàn.",
                },
                new Activity
                {
                    ActivityId = 8,
                    ActivityType = ActivityType.Rules,
                    ActivityName = "Quy định trên biển",
                    Description = "Du khách không được vứt rác bừa bãi trên bãi biển.",
                },
                new Activity
                {
                    ActivityId = 9,
                    ActivityType = ActivityType.Schedule,
                    ActivityName = "Tham quan san hô",
                    Description = "Lặn biển ngắm san hô và khám phá đáy biển trong 2 giờ.",
                },

                // Hoạt động cho tour "Tham quan bảo tàng"
                new Activity
                {
                    ActivityId = 10,
                    ActivityType = ActivityType.Services,
                    ActivityName = "Hướng dẫn viên bảo tàng",
                    Description = "Hướng dẫn chi tiết về các hiện vật quan trọng trong bảo tàng.",
                },
                new Activity
                {
                    ActivityId = 11,
                    ActivityType = ActivityType.Rules,
                    ActivityName = "Quy định tại bảo tàng",
                    Description = "Không được chạm vào hiện vật và giữ trật tự trong bảo tàng.",
                },
                new Activity
                {
                    ActivityId = 12,
                    ActivityType = ActivityType.Schedule,
                    ActivityName = "Lịch trình tham quan bảo tàng",
                    Description = "Khám phá bảo tàng trong khoảng thời gian 3 giờ.",
                },

                // Hoạt động cho tour "Cắm trại rừng"
                new Activity
                {
                    ActivityId = 13,
                    ActivityType = ActivityType.Services,
                    ActivityName = "Thuê lều và dụng cụ",
                    Description = "Cung cấp các dịch vụ thuê lều và dụng cụ cắm trại đầy đủ.",
                },
                new Activity
                {
                    ActivityId = 14,
                    ActivityType = ActivityType.Rules,
                    ActivityName = "Quy định cắm trại",
                    Description = "Không gây tiếng ồn sau 10 giờ tối và tuân thủ các quy định an toàn.",
                },
                new Activity
                {
                    ActivityId = 15,
                    ActivityType = ActivityType.Schedule,
                    ActivityName = "Tham quan rừng",
                    Description = "Đi bộ ngắm cảnh rừng và nghỉ qua đêm tại khu vực cắm trại.",
                },

                // Hoạt động cho tour "Kỳ nghỉ biển"
                new Activity
                {
                    ActivityId = 16,
                    ActivityType = ActivityType.Services,
                    ActivityName = "Dịch vụ đưa đón",
                    Description = "Dịch vụ xe đưa đón từ sân bay và các điểm đón đến khu resort.",
                },
                new Activity
                {
                    ActivityId = 17,
                    ActivityType = ActivityType.Rules,
                    ActivityName = "Quy định an toàn",
                    Description = "Du khách không được mang theo các vật dụng nguy hiểm vào khu nghỉ dưỡng.",
                },
                new Activity
                {
                    ActivityId = 18,
                    ActivityType = ActivityType.Schedule,
                    ActivityName = "Nghỉ dưỡng tại resort",
                    Description = "Trải nghiệm nghỉ dưỡng và tham gia các hoạt động giải trí tại resort.",
                }
            );

            // Cập nhật bảng DateStart cho các tour có IsFullDay = false
            modelBuilder.Entity<DateStart>().HasData(
                // TourId = 2 (Khám phá thiên nhiên)
                new DateStart
                {
                    DateStartId = 1,
                    StartDate = new DateTime(2024, 12, 5), // Ngày bắt đầu cho Tour Khám phá thiên nhiên
                    TypeStatus = StatusType.Pending,
                    TourId = 2
                },
                new DateStart
                {
                    DateStartId = 2,
                    StartDate = new DateTime(2024, 12, 10), // Ngày bắt đầu khác cho Tour Khám phá thiên nhiên
                    TypeStatus = StatusType.Pending,
                    TourId = 2
                },
                // TourId = 3 (Du lịch biển)
                new DateStart
                {
                    DateStartId = 3,
                    StartDate = new DateTime(2024, 12, 8), // Ngày bắt đầu cho Tour Du lịch biển
                    TypeStatus = StatusType.Pending,
                    TourId = 3
                },
                new DateStart
                {
                    DateStartId = 4,
                    StartDate = new DateTime(2024, 12, 12), // Ngày bắt đầu khác cho Tour Du lịch biển
                    TypeStatus = StatusType.Pending,
                    TourId = 3
                },
                // TourId = 5 (Cắm trại rừng)
                new DateStart
                {
                    DateStartId = 5,
                    StartDate = new DateTime(2024, 12, 15), // Ngày bắt đầu cho Tour Cắm trại rừng
                    TypeStatus = StatusType.Pending,
                    TourId = 5
                },
                new DateStart
                {
                    DateStartId = 6,
                    StartDate = new DateTime(2024, 12, 20), // Ngày bắt đầu khác cho Tour Cắm trại rừng
                    TypeStatus = StatusType.Pending,
                    TourId = 5
                }
            );
        }
    }
}
