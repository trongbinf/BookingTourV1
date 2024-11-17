using BookingTour.Business.Service.IService;
using BookingTour.Model;
using BookingTour.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingTour.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookingController : ControllerBase
	{
		private readonly IBookingService _bookingService;

		public BookingController(IBookingService bookingService)
		{
			_bookingService = bookingService;
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
		{
			var bookings = await _bookingService.GetAllAsync();

	
			return Ok(bookings);
		}


		[HttpPost("add")]
		public async Task<ActionResult> AddBooking([FromBody] BookingVm bookingVm)
		{
			if (bookingVm == null)
			{
				return BadRequest("Invalid booking data.");
			}

		
			if (TimeSpan.TryParse(bookingVm.StartTime, out var startTime))
			{
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
				return Ok(new
				{
					message = "Booking has added successfully !"
				});

			}
			else
			{
				return BadRequest("Invalid StartTime format.");
			}
		}




		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateBooking(int id, [FromBody] Booking booking)
		{
			if (id != booking.BookingId)
			{
				return BadRequest("ID mismatch.");
			}

			var existingBooking = await _bookingService.GetByIdAsync(id);
			if (existingBooking == null)
			{
				return NotFound($"Booking with ID {id} not found.");
			}

			await _bookingService.UpdateAsync(booking);
			return NoContent();
		}

		
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteBooking(int id)
		{
			var booking = await _bookingService.GetByIdAsync(id);
			if (booking == null)
			{
				return NotFound($"Booking with ID {id} not found.");
			}

			await _bookingService.DeleteAsync(id);
			return NoContent();
		}
	}
}
