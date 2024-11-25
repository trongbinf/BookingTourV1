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
		[HttpGet("{id}")]
		public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookingById(int id)
		{
			var bookings = await _bookingService.GetFirstOrDefaultAsync(x => x.BookingId == id, includeProperties: "Tour, User");
			return Ok(bookings);
		}



		[HttpPost("add")]
	
		public async Task<ActionResult> AddBooking([FromBody] BookingVm bookingVm)
		{
			if (bookingVm == null)
			{
				return BadRequest("Invalid booking data.");
			}

			TimeSpan startTime;
			switch (bookingVm.StartTime)
			{
				case "08:00:00":
					startTime = new TimeSpan(0, 8, 0, 0);
					break;
				case "09:00:00":
					startTime = new TimeSpan(0, 9, 0, 0);
					break;
				case "10:00:00":
					startTime = new TimeSpan(10, 0, 0,0);
					break;
				default:
					return BadRequest("Invalid StartTime. Allowed values are 08:00:00, 09:00:00, or 10:00:00.");
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
				case 6:
					statusType = StatusType.Completed;
					break;
				default:
					return BadRequest("Invalid status value. Please provide a valid status.");
			}



			booking.Status = statusType;
			await _bookingService.UpdateAsync(booking);			

			if (statusType == StatusType.Confirmed) 
			{
				var user = await _userManager.FindByIdAsync(booking.UserId);
				if (user == null)
				{
					return NotFound($"User with ID {booking.UserId} not found.");
				}

				var tour = await _tourService.GetByIdAsync(booking.TourId);
				if (tour == null)
				{
					return NotFound($"Tour with ID {booking.TourId} not found.");
				}

				var trackingLink = $"http://localhost:4200/profile"; 

				
				var confirmationContent = $@"
						<!DOCTYPE html>
						<html lang='en'>
						<head>
							<meta charset='UTF-8'>
							<meta name='viewport' content='width=device-width, initial-scale=1.0'>
							<title>Tour Confirmation</title>
							<style>
								body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; }}
								.email-container {{ max-width: 600px; margin: 20px auto; background: #ffffff; padding: 20px; border: 1px solid #ddd; border-radius: 5px; }}
								.header {{ text-align: center; background-color: #4CAF50; color: #ffffff; padding: 10px; }}
								.content {{ margin: 20px 0; font-size: 16px; color: #333; }}
								.content a {{ color: #4CAF50; text-decoration: none; font-weight: bold; }}
								.footer {{ text-align: center; padding: 10px; color: #666; }}
							</style>
						</head>
						<body>
							<div class='email-container'>
								<div class='header'>
									Tour Confirmation
								</div>
								<div class='content'>
									<p>Dear {user.UserName},</p>
									<p>We are excited to confirm your booking for the tour <strong>{tour.TourName}</strong>!</p>
									<p>Your booking has been successfully confirmed, and we are looking forward to hosting you.</p>
									<p>For more details or to track your booking, please visit your <a href='{trackingLink}'>Booking history</a>.</p>
									<p>We hope you have an amazing experience on your tour!</p>
								</div>
								<div class='footer'>
									&copy; 2024 Booking Tour Travel. All rights reserved.
								</div>
							</div>
						</body>
						</html>";

				// Create and send the confirmation email
				var confirmationMessage = new Message(new string[] { user.Email }, "Your Tour Has Been Confirmed!", confirmationContent);
				_emailService.SendEmail(confirmationMessage);
			}


			if (statusType == StatusType.Completed)
			{
				var user = await _userManager.FindByIdAsync(booking.UserId);
				if (user == null)
				{
					return NotFound($"User with ID {booking.UserId} not found.");
				}

			
				var tour = await _tourService.GetByIdAsync(booking.TourId);
				if (tour == null)
				{
					return NotFound($"Tour with ID {booking.TourId} not found.");
				}
			
				var reviewLink = $"http://localhost:4200/review-add/{tour.TourId}/{booking.BookingId}"; 

				var reviewContent = $@"
			<!DOCTYPE html>
			<html lang='en'>
			<head>
				<meta charset='UTF-8'>
				<meta name='viewport' content='width=device-width, initial-scale=1.0'>
				<title>Review Your Experience</title>
				<style>
					body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; }}
					.email-container {{ max-width: 600px; margin: 20px auto; background: #ffffff; padding: 20px; border: 1px solid #ddd; border-radius: 5px; }}
					.header {{ text-align: center; background-color: #4CAF50; color: #ffffff; padding: 10px; }}
					.content {{ margin: 20px 0; font-size: 16px; color: #333; }}
					.content a {{ color: #4CAF50; text-decoration: none; font-weight: bold; }}
					.footer {{ text-align: center; padding: 10px; color: #666; }}
				</style>
			</head>
			<body>
				<div class='email-container'>
					<div class='header'>
						We Value Your Feedback!
					</div>
					<div class='content'>
						<p>Dear {user.UserName},</p>
						<p>Thank you for choosing <strong>{tour.TourName}</strong>. We hope you had a wonderful experience!</p>
						<p>We would greatly appreciate it if you could take a moment to leave a review about your experience. Your feedback helps us improve our services and assist other travelers in making informed decisions.</p>
						<p><a href='{reviewLink}'>Click here to leave your review</a></p>
					</div>
					<div class='footer'>
						&copy; 2024 Booking Tour Travel.
					</div>
				</div>
			</body>
			</html>";

				// Send email
				var reviewMessage = new Message(new string[] { user.Email }, "We Value Your Feedback!", reviewContent);
				_emailService.SendEmail(reviewMessage);
			}


			return Ok(new { message = "Booking status updated successfully." });
		}


		[HttpGet("search")]
	
		public async Task<ActionResult<PaginatedList<BookingVm>>> SearchBooking(
							[FromQuery] string? key = null,
							[FromQuery] int pageIndex = 1,
							[FromQuery] int pageSize = 5)
		{
			
			var bookings = await _bookingService.GetAllAsync(includeProperties: "Tour,User");

		
			if (!string.IsNullOrEmpty(key))
			{
				bookings = bookings.Where(b =>
					(b.Tour != null && b.Tour.TourName.Contains(key, StringComparison.OrdinalIgnoreCase)) ||
					(b.User != null && b.User.UserName.Contains(key, StringComparison.OrdinalIgnoreCase))
				).ToList();
			}

		
			var totalRecords = bookings.Count();

		
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


		[HttpGet("by-user-id")]
		
		public async Task<ActionResult<PaginatedList<BookingVm>>> GetBookingsByUserId(
		[FromQuery] string userId,
		[FromQuery] int pageIndex = 1,
		[FromQuery] int pageSize = 5)
		{
			if (string.IsNullOrEmpty(userId))
			{
				return BadRequest("UserId is required.");
			}

			var bookings = await _bookingService.GetAllAsync(x => x.UserId == userId, includeProperties: "Tour,User");

			var totalRecords = bookings.Count();

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
					Tour = b.Tour,
					User = b.User
				})
				.ToList();

			var paginatedList = new PaginatedList<Booking>(
				paginatedBookings, totalRecords, pageIndex, pageSize);

			return Ok(paginatedList);
		}

		[HttpGet("total-money-by-user")]
		public async Task<ActionResult<decimal>> GetTotalMoneyByUser([FromQuery] string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				return BadRequest("UserId is required.");
			}
		
			var bookings = await _bookingService.GetAllAsync(x => x.UserId == userId, includeProperties: "Tour");
			if (bookings == null || !bookings.Any())
			{
				return NotFound($"No bookings found for user with ID: {userId}");
			}

			var totalMoney = bookings.Sum(booking => booking.PersonNumber * booking.Tour.Price);
		

			return Ok(totalMoney);
		}

	}


}
