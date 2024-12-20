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
		public async Task<ActionResult<List<TourVm>>> GetToursByCategory(
			string categoryName,
			int pageIndex = 1,
			int pageSize = 6,
			int? tourId = null) // Thêm tham số tourId
		{
			string normalName = rm.RemoveUnicode(categoryName);
			var tours = await _tourService.GetAllAsync(
				filter: t => t.Category.Name == categoryName && (!tourId.HasValue || t.TourId != tourId.Value),
				includeProperties: "Category,DateStarts,Reviews,Activities,Bookings"
			);

			// Lọc các tour dựa trên trạng thái và loại bỏ tourID nếu có
			var tourVms = tours
				.Where(t => t.Status)
				.Select(tour => new TourVm
				{
					Tour = tour,
					DateStarts = tour.DateStarts,
					Category = tour.Category,
					Reviews = tour.Reviews,
					Activities = tour.Activities,
					Bookings = tour.Bookings
				})
				.ToList();

			var response = rm.ToPaginatedList(tourVms, pageIndex, pageSize);

			return Ok(response);
		}



		// code's dong
		[HttpGet("filter")]
        public async Task<ActionResult<PaginatedList<TourVm>>> FilterTour(
            [FromQuery] string? city = null,
            [FromQuery] string? country = null,
            [FromQuery] string? category = null,
            [FromQuery] double? minPrice = null,
            [FromQuery] double? maxPrice = null,
            [FromQuery] string? durations = null,
            [FromQuery] string? activity = null,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 8)
        {
            var tours = await _tourService.GetAllAsync(
                includeProperties: "Category,Reviews,Activities,Bookings,DateStarts"
                );

            tours = tours.Where(t => t.Status).ToList();

            // Fliter
            tours = FilterByCity(tours, city);
            tours = FilterByCountry(tours, country);
            tours = FilterByCategory(tours, category);
            tours = FilterByPriceRange(tours, minPrice, maxPrice);
            tours = FilterByDurations(tours, durations);
            tours = FilterByActivity(tours, activity);

            var result = tours.Select(t => new TourVm
            {
                Tour = t,
                Category = t.Category,
                Activities = t.Activities,
                Bookings = t.Bookings,
                DateStarts = t.DateStarts,
                MainImage = null,
                DetailImages = null
            }).ToList();

            var paginatedResult = rm.ToPaginatedList(result, pageIndex, pageSize);
            return Ok(paginatedResult);
        }

        // search 
        [HttpGet("search")]
        public async Task<ActionResult<PaginatedList<TourVm>>> SearchByName(
            [FromQuery] string name, [FromQuery] string dateRange,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 8)
        {
            var tours = await _tourService.GetAllAsync(
                includeProperties: "Category,Reviews,Activities,Bookings,DateStarts"
                );
            tours = tours.Where(t => t.Status).ToList();
            tours = FilterByName(tours, name);
            tours = FilterByDateRange(tours, dateRange);

            var result = tours.Select(t => new TourVm
            {
                Tour = t,
                Category = t.Category,
                Activities = t.Activities,
                Bookings = t.Bookings,
                DateStarts = t.DateStarts,
                MainImage = null,
                DetailImages = null
            }).ToList();

            var paginatedResult = rm.ToPaginatedList(result, pageIndex, pageSize);
            return Ok(paginatedResult);
        }


        // Filter by name tour
        private IEnumerable<Tour> FilterByName(IEnumerable<Tour> tours, string? name)
        {
            if (string.IsNullOrEmpty(name)) return tours;

            var normalizedName = rm.RemoveUnicode(name);
            return tours.Where(t =>
                rm.RemoveUnicode(t.TourName).Contains(normalizedName, StringComparison.OrdinalIgnoreCase) ||
                rm.RemoveUnicode(t.City).Contains(normalizedName, StringComparison.OrdinalIgnoreCase) ||
                rm.RemoveUnicode(t.Country).Contains(normalizedName, StringComparison.OrdinalIgnoreCase) ||
                rm.RemoveUnicode(t.Category.Name).Contains(normalizedName, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        // Filter by city
        private IEnumerable<Tour> FilterByCity(IEnumerable<Tour> tours, string? city)
        {
            if (string.IsNullOrEmpty(city)) return tours;

            var normalizedCity = rm.RemoveUnicode(city);
            return tours.Where(t => rm.RemoveUnicode(t.City).Contains(normalizedCity, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // filter by country
        private IEnumerable<Tour> FilterByCountry(IEnumerable<Tour> tours, string? country)
        {
            if (string.IsNullOrEmpty(country)) return tours;

            var normalizedCountry = rm.RemoveUnicode(country);
            return tours.Where(t => rm.RemoveUnicode(t.Country).Contains(normalizedCountry, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // filter by category
        private IEnumerable<Tour> FilterByCategory(IEnumerable<Tour> tours, string? category)
        {
            if (string.IsNullOrEmpty(category)) return tours;

            var normalizedCategory = rm.RemoveUnicode(category);
            return tours.Where(t => rm.RemoveUnicode(t.Category.Name).Contains(normalizedCategory, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Filter by  price
        private IEnumerable<Tour> FilterByPriceRange(IEnumerable<Tour> tours, double? minPrice, double? maxPrice)
        {
            if (minPrice.HasValue) tours = tours.Where(t => t.Price >= minPrice.Value).ToList();
            if (maxPrice.HasValue) tours = tours.Where(t => t.Price <= maxPrice.Value).ToList();

            return tours;
        }

        // Filter by date range
        private IEnumerable<Tour> FilterByDateRange(IEnumerable<Tour> tours, string? dateRange)
        {
            if (string.IsNullOrEmpty(dateRange)) return tours;

            var dates = dateRange.Split("to", StringSplitOptions.TrimEntries);
            if (dates.Length == 2 &&
                DateOnly.TryParse(dates[0], out var startDate) &&
                DateOnly.TryParse(dates[1], out var endDate))
            {
                Console.WriteLine(startDate);
                Console.WriteLine(endDate);
                return tours.Where(t => (t.IsFullDay == true) || (t.DateStarts.Any(ds => ds.StartDate >= startDate && ds.StartDate <= endDate))).ToList();
            }

            return tours;
        }

        // Filter by duration
        private IEnumerable<Tour> FilterByDurations(IEnumerable<Tour> tours, string? durations)
        {
            if (string.IsNullOrEmpty(durations)) return tours;

            var durationList = durations.Split(",", StringSplitOptions.TrimEntries);
            return tours.Where(t => durationList.Any(d => t.Duration.Contains(d, StringComparison.OrdinalIgnoreCase))).ToList();
        }

        // Filter by activity
        private IEnumerable<Tour> FilterByActivity(IEnumerable<Tour> tours, string? activity)
        {
            if (string.IsNullOrEmpty(activity)) return tours;

            var activities = activity.Split(",", StringSplitOptions.TrimEntries);
            return tours.Where(t =>
                activities.Any(act =>
                    t.Activities.Any(a =>
                        a.ActivityName.Contains(act, StringComparison.OrdinalIgnoreCase) ||
                        a.Description.Contains(act, StringComparison.OrdinalIgnoreCase)
                    )
                )
            ).ToList();
        }

        [HttpGet("random")]
        public async Task<ActionResult<IEnumerable<TourVm>>> GetRamdonly()
        {
            var tours = await _tourService.GetAllAsync(
               includeProperties: "Category,Reviews,Activities,Bookings,DateStarts"
           );
            var randomTour = tours.OrderBy(_ => Guid.NewGuid()).Take(6).ToList();
            var randomTourVms = randomTour.Select(tour => new TourVm
            {
                Tour = tour,
                DateStarts = tour.DateStarts,
                Category = tour.Category,
                Reviews = tour.Reviews,
                Activities = tour.Activities,
                Bookings = tour.Bookings
            });

            return Ok(randomTourVms);
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
                DateStarts = tour.DateStarts
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
        [DisableRequestSizeLimit]
        public async Task<ActionResult> CreateTour(TourAddVm tourAddVm)
        {
            if (tourAddVm == null)
            {
                return BadRequest("Invalid tour data.");
            }
            var newTour = new Tour
            {
                TourName = tourAddVm.TourName,
                Description = tourAddVm.Description,
                City = tourAddVm.City,
                Country = tourAddVm.Country,
                Duration = tourAddVm.Duration,
                IsFullDay = tourAddVm.IsFullDay,
                Price = tourAddVm.Price,
                PersonNumber = tourAddVm.PersonNumber,
                Status = tourAddVm.Status,
                CategoryId = tourAddVm.CategoryId,

            };
            if (tourAddVm.MainImage != null)
            {
                newTour.MainImage = await SaveImageAsync(tourAddVm.MainImage);
            }

            if (tourAddVm.DetailImages != null && tourAddVm.DetailImages.Any())
            {
                var imagePaths = new List<string>();
                foreach (var file in tourAddVm.DetailImages)
                {
                    imagePaths.Add(await SaveImageAsync(file));
                }
                newTour.OtherImage = string.Join("; ", imagePaths);
            }

            await _tourService.AddAsync(newTour);
            return CreatedAtAction(nameof(GetTourById), new { id = newTour.TourId }, newTour);


        }

        // Hàm lưu ảnh Tamnx
        private async Task<string> SaveImageAsync(IFormFile file)
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            var uploadsFolder = Path.Combine( currentDirectory,"..","..","UI","src","assets","img","tour");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }


            return Path.Combine("assets", "img", "tour", file.FileName);
        }



        [HttpPut("{id}")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult> UpdateTour(int id, TourAddVm tourAddVm)
        {
            if (tourAddVm == null)
                return BadRequest("Invalid tour data.");
            if (tourAddVm.TourId != id)
                return BadRequest("TourId in request body does not match the route.");
            var existingTour = await _tourService.GetByIdAsync(id);
            if (existingTour == null)
                return NotFound("Tour not found.");
            existingTour.TourName = tourAddVm.TourName;
            existingTour.Description = tourAddVm.Description;
            existingTour.City = tourAddVm.City;
            existingTour.Country = tourAddVm.Country;
            existingTour.Duration = tourAddVm.Duration;
            existingTour.IsFullDay = tourAddVm.IsFullDay;
            existingTour.Price = tourAddVm.Price;
            existingTour.PersonNumber = tourAddVm.PersonNumber;
            existingTour.Status = tourAddVm.Status;
            existingTour.CategoryId = tourAddVm.CategoryId;
            if (tourAddVm.MainImage != null)
            {
                existingTour.MainImage = await SaveImageAsync(tourAddVm.MainImage);
            }
            if (tourAddVm.DetailImages != null && tourAddVm.DetailImages.Any())
            {
                var imagePaths = new List<string>();
                foreach (var file in tourAddVm.DetailImages)
                {
                    imagePaths.Add(await SaveImageAsync(file));
                }
                existingTour.OtherImage = string.Join("; ", imagePaths);
            }
            await _tourService.UpdateAsync(existingTour);

            return Ok(new { message = "Tour updated successfully", updatedTour = existingTour });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTour(int id)
        {
            var existingTour = await _tourService.GetByIdAsync(id);
            if (existingTour == null)
            {
                return NotFound();
            }

            existingTour.Status = !existingTour.Status;

            await _tourService.UpdateAsync(existingTour);
            return NoContent();
        }
    }
}
