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


			modelBuilder.Entity<Category>().HasData(
				new Category { CategoryId = 1, Name = "Tour tham quan", Status = true },
				new Category { CategoryId = 2, Name = "Thiên nhiên", Status = true },
				new Category { CategoryId = 3, Name = "Biển", Status = true }
			);


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
					PersonNumber = 30,
					Status = true,
					Created = DateTime.Now,
					Duration = "1 ngày 1 đêm",
					CategoryId = 1
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
					PersonNumber = 16,
					Status = true,
					Created = DateTime.Now,
					Duration = "2 ngày 1 đêm",
					CategoryId = 2
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
					PersonNumber = 19,
					Status = true,
					Created = DateTime.Now,
					Duration = "3 ngày 2 đêm",
					CategoryId = 3
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
					PersonNumber = 19,
					Status = true,
					Created = DateTime.Now,
					Duration = "1 ngày 1 đêm",
					CategoryId = 1
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
					PersonNumber = 15,
					Status = true,
					Created = DateTime.Now,
					Duration = "2 ngày 2 đêm",
					CategoryId = 2
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
					PersonNumber = 26,
					Status = true,
					Created = DateTime.Now,
					Duration = "4 ngày 3 đêm",
					CategoryId = 3
				}
			);

			// Thêm dữ liệu cho bảng Activity và liên kết với các tour
			modelBuilder.Entity<Activity>().HasData(
				 new Activity
				 {
					 ActivityId = 1,
					 ActivityType = ActivityType.Services,
					 ActivityName = "Ăn sáng tại khách sạn",
					 Description = "Thưởng thức bữa sáng buffet tại khách sạn trước khi khởi hành.",
					 Duration = new TimeSpan(0, 1, 0, 0), // 1 giờ
					 Location = "Khách sạn trung tâm Hà Nội",
					 TourId = 1
				 },
				new Activity
				{
					ActivityId = 2,
					ActivityType = ActivityType.Rules,
					ActivityName = "Hướng dẫn tham quan",
					Description = "Hướng dẫn viên cung cấp thông tin về các địa điểm nổi bật.",
					Duration = new TimeSpan(0, 2, 0, 0), // 2 giờ
					Location = "Điểm tham quan Hà Nội",
					TourId = 1
				},
				new Activity
				{
					ActivityId = 3,
					ActivityType = ActivityType.Schedule,
					ActivityName = "Tham quan Văn Miếu Quốc Tử Giám",
					Description = "Khám phá di tích lịch sử với kiến trúc độc đáo.",
					Duration = new TimeSpan(0, 3, 0, 0), // 3 giờ
					Location = "Văn Miếu Quốc Tử Giám",
					TourId = 1
				},
				new Activity
				{
					ActivityId = 4,
					ActivityType = ActivityType.Services,
					ActivityName = "Dùng bữa trưa tại nhà hàng",
					Description = "Thưởng thức ẩm thực truyền thống tại nhà hàng nổi tiếng.",
					Duration = new TimeSpan(0, 1, 30, 0), // 1.5 giờ
					Location = "Nhà hàng Phố Cổ Hà Nội",
					TourId = 1
				},
				new Activity
				{
					ActivityId = 5,
					ActivityType = ActivityType.Schedule,
					ActivityName = "Mua sắm tại chợ Đồng Xuân",
					Description = "Tự do tham quan và mua sắm các đặc sản địa phương.",
					Duration = new TimeSpan(0, 2, 0, 0), // 2 giờ
					Location = "Chợ Đồng Xuân, Hà Nội",
					TourId = 1
				},

				// Hoạt động cho tour "Khám phá thiên nhiên" (TourId = 2)
				new Activity
				{
					ActivityId = 6,
					ActivityType = ActivityType.Services,
					ActivityName = "Ăn sáng tại khu nghỉ dưỡng",
					Description = "Bữa sáng với các món ăn đặc sản địa phương.",
					Duration = new TimeSpan(0, 1, 0, 0), // 1 giờ
					Location = "Khu nghỉ dưỡng Đà Lạt",
					TourId = 2
				},
				new Activity
				{
					ActivityId = 7,
					ActivityType = ActivityType.Schedule,
					ActivityName = "Khám phá thác Datanla",
					Description = "Tham quan và chụp ảnh tại thác nước nổi tiếng.",
					Duration = new TimeSpan(0, 3, 0, 0), // 3 giờ
					Location = "Thác Datanla, Đà Lạt",
					TourId = 2
				},
				new Activity
				{
					ActivityId = 8,
					ActivityType = ActivityType.Services,
					ActivityName = "Dùng bữa trưa ngoài trời",
					Description = "Bữa trưa BBQ tại khu vực gần thác nước.",
					Duration = new TimeSpan(0, 2, 0, 0), // 2 giờ
					Location = "Khu vực cắm trại, Đà Lạt",
					TourId = 2
				},
				new Activity
				{
					ActivityId = 9,
					ActivityType = ActivityType.Schedule,
					ActivityName = "Đi xe đạp quanh hồ Tuyền Lâm",
					Description = "Thư giãn với chuyến đi xe đạp quanh hồ.",
					Duration = new TimeSpan(0, 2, 0, 0), // 2 giờ
					Location = "Hồ Tuyền Lâm, Đà Lạt",
					TourId = 2
				},
				new Activity
				{
					ActivityId = 10,
					ActivityType = ActivityType.Rules,
					ActivityName = "Hướng dẫn bảo vệ môi trường",
					Description = "Thực hiện các quy định bảo vệ môi trường khu tham quan.",
					Duration = new TimeSpan(0, 0, 30, 0), // 30 phút
					Location = "Khu vực thiên nhiên, Đà Lạt",
					TourId = 2
				}
			);

			modelBuilder.Entity<DateStart>().HasData(
				new DateStart
				{
					DateStartId = 1,
					StartDate = new DateOnly(2024, 11, 5), 
					TypeStatus = Model.Enum.StatusType.Available, 
					TourId = 2 
				},
				new DateStart
				{
					DateStartId = 2,
					StartDate = new DateOnly(2024, 11, 10),
					TypeStatus = Model.Enum.StatusType.Available,
					TourId = 2
				},
				new DateStart
				{
					DateStartId = 3,
					StartDate = new DateOnly(2024, 11, 15),
					TypeStatus = Model.Enum.StatusType.Available,
					TourId = 2
				}
			);

		

		}
	}
}
