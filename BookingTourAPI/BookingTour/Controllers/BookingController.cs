using BookingTour.Business.Service.IService;
using BookingTour.Model;
using BookingTour.Model.Enum;
using BookingTour.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BookingTour.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookingController : ControllerBase
	{
		private readonly IBookingService _bookingService;
		private readonly ITourService _tourService;
		private readonly IEmailService _emailService;
		private readonly UserManager<AppUser> _userManager;

		public BookingController(IBookingService bookingService,
			IEmailService emailService,
			UserManager<AppUser> userManager,
			ITourService tourService)
		{
			_bookingService = bookingService;
			_emailService = emailService;
			_userManager = userManager;
			_tourService = tourService;
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
		{
			var bookings = await _bookingService.GetAllAsync(includeProperties: "Tour, User");
			return Ok(bookings);
		}

		[HttpPost("add")]
		[Authorize]
		public async Task<ActionResult> AddBooking([FromBody] BookingVm bookingVm)
		{
			if (bookingVm == null)
			{
				return BadRequest("Invalid booking data.");
			}

			if (!TimeSpan.TryParse(bookingVm.StartTime, out var startTime))
			{
				return BadRequest("Invalid StartTime format.");
			}

			var booking = new Booking
			{
				BookingDate = bookingVm.BookingDate,
				PickDate = bookingVm.PickDate,
				StartTime = startTime,
				PersonNumber = bookingVm.PersonNumber,
				Notes = bookingVm.Notes,
				Status = bookingVm.Status,
				TourId = bookingVm.TourId,
				UserId = bookingVm.UserId
			};

			await _bookingService.AddAsync(booking);

			var user = await _userManager.FindByIdAsync(booking.UserId);
			if (user == null)
			{
				return NotFound($"User with ID {booking.UserId} not found.");
			}

			// Giả định bạn lấy thông tin tour từ một dịch vụ TourService
			var tour = await _tourService.GetByIdAsync(booking.TourId);
			if (tour == null)
			{
				return NotFound($"Tour with ID {booking.TourId} not found.");
			}

			// Tính tổng tiền và định dạng sang VND
			var totalMoney = booking.PersonNumber * tour.Price;
			var totalMoneyVND = totalMoney.ToString("C0", CultureInfo.GetCultureInfo("vi-VN"));

			var confirmationLink = $"http://localhost:4200/profile";
			var content = $@"
				<!DOCTYPE html>
				<html lang='en'>
				<head>
					<meta charset='UTF-8'>
					<meta name='viewport' content='width=device-width, initial-scale=1.0'>
					<title>Booking Confirmation</title>
					<style>
						body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; }}
						.email-container {{ max-width: 600px; margin: 20px auto; background: #ffffff; padding: 20px; border: 1px solid #ddd; border-radius: 5px; }}
						.header {{ text-align: center; background-color: #4CAF50; color: #ffffff; padding: 10px; }}
						.content {{ margin: 20px 0; font-size: 16px; color: #333; }}
						.content a {{ color: #4CAF50; text-decoration: none; }}
						.footer {{ text-align: center; padding: 10px; color: #666; }}
					</style>
				</head>
				<body>
					<div class='email-container'>
						<div class='header'>
							Booking Confirmation
						</div>
						<div class='content'>
							<p>Dear {user.UserName},</p>
							<p>Thank you for your booking. Here are the details of your booking:</p>
							<ul>
								<li><strong>Tour Name:</strong> {tour.TourName}</li>
								<li><strong>Number of People:</strong> {booking.PersonNumber} people</li>
								<li><strong>Pick Date:</strong> {booking.PickDate:yyyy-MM-dd}</li>
								<li><strong>Start Time:</strong> {booking.StartTime:hh\\:mm}</li>
								<li><strong>Price (per person):</strong> {tour.Price.ToString("C0", CultureInfo.GetCultureInfo("vi-VN"))}</li>
								<li><strong>Total Money:</strong> {totalMoneyVND}</li>
							</ul>
							<p><a href='{confirmationLink}'>View Your Booking</a></p>
						</div>
						<div class='footer'>
							&copy; 2024 Booking Tour Travel.
						</div>
					</div>
				</body>
				</html>";


			var newMessage = new Message(new string[] { user.Email }, "Booking Confirmation", content);
			_emailService.SendEmail(newMessage);

			return Ok(new { message = "Booking has been added successfully!" });
		}




		[HttpPut("update-status/{bookingId}")]
		public async Task<ActionResult> UpdateBookingStatusByBookingId(int bookingId, int status)
		{
			var booking = await _bookingService.GetByIdAsync(bookingId);
			if (booking == null)
			{
				return NotFound($"No booking found for BookingId: {bookingId}");
			}

			StatusType statusType;
	
			switch (status)
			{
				case 0:
					statusType = StatusType.Submited;
					break;
				case 1:
					statusType = StatusType.Pending;
					break;
				case 2:
					statusType = StatusType.Confirmed;
					break;
				case 3:
					statusType = StatusType.Canceled;
					break;
				case 4:
					statusType = StatusType.Available;
					break;
				case 5:
					statusType = StatusType.Unavailable;
					break;
				default:
					return BadRequest("Invalid status value. Please provide a valid status.");
			}

			
		
			booking.Status = statusType;
		
			await _bookingService.UpdateAsync(booking);

			return Ok(new { message = "Booking status updated successfully." });
		}

		[HttpGet("search")]
		public async Task<ActionResult<PaginatedList<BookingVm>>> SearchBooking(
							[FromQuery] string? key = null,
							[FromQuery] int pageIndex = 1,
							[FromQuery] int pageSize = 5)
		{
			// Lấy danh sách Booking kèm thông tin Tour và User qua includeProperties
			var bookings = await _bookingService.GetAllAsync(includeProperties: "Tour,User");

			// Lọc theo từ khóa nếu có
			if (!string.IsNullOrEmpty(key))
			{
				bookings = bookings.Where(b =>
					(b.Tour != null && b.Tour.TourName.Contains(key, StringComparison.OrdinalIgnoreCase)) ||
					(b.User != null && b.User.UserName.Contains(key, StringComparison.OrdinalIgnoreCase))
				).ToList();
			}

			// Tính tổng số bản ghi sau khi lọc
			var totalRecords = bookings.Count();

			// Phân trang
			var paginatedBookings = bookings
				.Skip((pageIndex - 1) * pageSize)
				.Take(pageSize)
				.Select(b => new Booking
				{
					BookingId = b.BookingId,
					BookingDate = b.BookingDate,
					PickDate = b.PickDate,
					StartTime = b.StartTime,
					PersonNumber = b.PersonNumber,
					Notes = b.Notes,
					Status = b.Status,
					Tour =b.Tour,
					User = b.User
				})
				.ToList();

			var paginatedList = new PaginatedList<Booking>(
				paginatedBookings, totalRecords, pageIndex, pageSize);

			return Ok(paginatedList);
		}




	}


}
