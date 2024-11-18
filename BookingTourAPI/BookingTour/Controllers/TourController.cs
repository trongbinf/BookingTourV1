﻿using BookingTour.Business.Service.IService;
using BookingTour.Data.Repository.IRepository;
using BookingTour.Model;
using BookingTour.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using rm = BookingTour.Business.Service.HandleTextUnicode;
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
            // dong adds datestarts
            var tours = await _tourService.GetAllAsync(
                includeProperties: "Category,Reviews,Activities,Bookings,DateStarts"
            );

            var tourVms = tours.Select(tour => new TourVm
            {
                Tour = tour,
                DateStarts = tour.DateStarts,
                Category = tour.Category,
                Reviews = tour.Reviews,
                Activities = tour.Activities,
                Bookings = tour.Bookings
            });

            return Ok(tourVms);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<List<TourVm>>> GetCategoriesWithTours(string categoryName,
            int pageIndex = 1, int pageSize = 6)
        {
            // Lấy toàn bộ dữ liệu các tour bao gồm category và các thuộc tính liên quan
            string normalName = rm.RemoveUnicode(categoryName);
            var tours = await _tourService.GetAllAsync(
                filter: t => t.Category.Name == categoryName,
                includeProperties: "Category,DateStarts,Reviews,Activities,Bookings"
            );

            var tourVm = tours.Where(t => t.Status == true).Select(t => new TourVm
            {
                Tour = t,
                Category = t.Category,
                Activities = t.Activities,
                DateStarts = t.DateStarts,
            }).ToList();

            var response = rm.ToPaginatedList<TourVm>(tourVm, pageIndex, pageSize);
            return Ok(response);
        }

        [HttpGet("categories/{categoryName}")]
        public async Task<ActionResult<IEnumerable<TourVm>>> GetToursByCategory(string categoryName,
            int pageIndex = 1, int pageSize = 6)
        {
            string normalName = rm.RemoveUnicode(categoryName);
            var tours = await _tourService.GetAllAsync(
                filter: t => t.Category.Name == categoryName,
                includeProperties: "Category,DateStarts,Reviews,Activities,Bookings"
            );

            // Lấy tối đa 5 tour theo category đã chọn
            var tourVms = tours.Select(tour => new TourVm
            {
                Tour = tour,
                DateStarts = tour.DateStarts,
                Category = tour.Category,
                Reviews = tour.Reviews,
                Activities = tour.Activities,
                Bookings = tour.Bookings
            }).ToList();

            var response = rm.ToPaginatedList<TourVm>(tourVms, pageIndex, pageSize);

            return Ok(response);
        }


        // code's dong
        [HttpGet("search")]
        public async Task<ActionResult<PaginatedList<TourVm>>> SearchTour(
            [FromQuery] string? name = null,
            [FromQuery] string? city = null,
            [FromQuery] string? country = null,
            [FromQuery] string? category = null,
            [FromQuery] double? minPrice = null,
            [FromQuery] double? maxPrice = null,
            [FromQuery] string? dateRange = null,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 8)
        {
            var listTour = await _tourService.GetAllAsync(
                includeProperties: "Category,Reviews,Activities,Bookings,DateStarts");

            listTour = listTour.Where(t => (t.Status == true)).ToList();
            if (!string.IsNullOrEmpty(name))
            {
                var normalizedName = rm.RemoveUnicode(name);
                listTour = listTour.Where(t =>
                    rm.RemoveUnicode(t.TourName).Contains(normalizedName, StringComparison.OrdinalIgnoreCase) ||
                    rm.RemoveUnicode(t.City).Contains(normalizedName, StringComparison.OrdinalIgnoreCase) ||
                    rm.RemoveUnicode(t.Country).Contains(normalizedName, StringComparison.OrdinalIgnoreCase) ||
                    rm.RemoveUnicode(t.Category.Name).Contains(normalizedName, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            if (!string.IsNullOrEmpty(city))
            {
                var normalizedCity = rm.RemoveUnicode(city);
                listTour = listTour.Where(t => rm.RemoveUnicode(t.City).Contains(normalizedCity, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(country))
            {
                var normalizedCountry = rm.RemoveUnicode(country);
                listTour = listTour.Where(t => rm.RemoveUnicode(t.Country).Contains(normalizedCountry, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(category))
            {
                var normalizedCategory = rm.RemoveUnicode(category);
                listTour = listTour.Where(t => rm.RemoveUnicode(t.Category.Name).Contains(normalizedCategory, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (minPrice.HasValue)
            {
                listTour = listTour.Where(t => t.Price >= minPrice.Value).ToList();
            }
            if (maxPrice.HasValue)
            {
                listTour = listTour.Where(t => t.Price <= maxPrice.Value).ToList();
            }

            if (!string.IsNullOrEmpty(dateRange))
            {
                var dates = dateRange.Split("to");
                if (dates.Length == 2 &&
                    DateOnly.TryParse(dates[0].Trim(), out var dateStart) &&
                     DateOnly.TryParse(dates[1].Trim(), out var dateEnd))
                {
                    listTour.Where(t => t.DateStarts.Any(ds => ds.StartDate >= dateStart
                    && ds.StartDate <= dateEnd) || t.IsFullDay == true).ToList();
                }
            }
            

            var resultOutput = listTour.Select(t => new TourVm
            {
                Tour = t,
                Bookings = t.Bookings,
                Category = t.Category,
                DateStarts = t.DateStarts,
                Activities = t.Activities,
                Reviews = t.Reviews,
                MainImage = null,
                DetailImages = null
            }).ToList();


            var response = rm.ToPaginatedList<TourVm>(resultOutput, pageIndex, pageSize);
            return Ok(response);
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<TourVm>> GetTourById(int id)
        {
            var tour = await _tourService.GetFirstOrDefaultAsync(x => x.TourId == id,
                includeProperties: "Category,Reviews,Activities,Bookings,DateStarts");
            if (tour == null)
            {
                return NotFound();
            }

            var tourVm = new TourVm
            {
                Tour = tour,
                Category = tour.Category,
                Reviews = tour.Reviews,
                Activities = tour.Activities,
                Bookings = tour.Bookings,
				DateStarts  = tour.DateStarts			
            };

            return Ok(tourVm);
        }

        [HttpGet("get-country")]
        public async Task<IActionResult> GetCountry()
        {
            var list = await _tourService.GetAllCountry();
            return Ok(list);
        }

        [HttpGet("get-city/{name}")]
        public async Task<IActionResult> GetCity(string name)
        {
            var list = await _tourService.GetAllCityByCountry(name);
            return Ok(list);
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
