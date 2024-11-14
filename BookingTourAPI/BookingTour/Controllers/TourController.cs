using BookingTour.Business.Service.IService;
using BookingTour.Model;
using BookingTour.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  // For Include method
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingTour.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TourController : ControllerBase
	{
		private readonly ITourService _tourService;

		public TourController(ITourService tourService)
		{
			_tourService = tourService;
		}

		
		[HttpGet]
		public async Task<ActionResult<IEnumerable<TourVm>>> GetAllTours()
		{
			var tours = await _tourService.GetAllAsync(
				includeProperties: "Category,Reviews,TourActivities,Bookings"
			);

			var tourVms = tours.Select(tour => new TourVm
			{
				Tour = tour,
				Category = tour.Category,
				Reviews = tour.Reviews,
				TourActivities = tour.TourActivities,
				Bookings = tour.Bookings
			});

			return Ok(tourVms); 
		}

	
		[HttpGet("{id}")]
		public async Task<ActionResult<TourVm>> GetTourById(int id)
		{
			var tour = await _tourService.GetFirstOrDefaultAsync( x => x.TourId == id, 
				includeProperties: "Category,Reviews,TourActivities,Bookings"); 
			if (tour == null)
			{
				return NotFound();  
			}

			var tourVm = new TourVm
			{
				Tour = tour,
				Category = tour.Category,
				Reviews = tour.Reviews,
				TourActivities = tour.TourActivities,
				Bookings = tour.Bookings
			};

			return Ok(tourVm);  
		}

	
		[HttpPost]
		public async Task<ActionResult> CreateTour(TourVm tour)
		{
			await _tourService.AddAsync(tour.Tour);
			return CreatedAtAction(nameof(GetTourById), new { id = tour.Tour.TourId }, tour);
		}

	
		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateTour(int id, TourVm tour)
		{
			if (id != tour.Tour.TourId)
			{
				return BadRequest("Tour ID mismatch");
			}

			await _tourService.UpdateAsync(tour.Tour);
			return NoContent();
		}

		
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteTour(int id)
		{
			var existingTour = await _tourService.GetByIdAsync(id);
			if (existingTour == null)
			{
				return NotFound();
			}

			await _tourService.DeleteAsync(id);
			return NoContent();
		}
	}
}
