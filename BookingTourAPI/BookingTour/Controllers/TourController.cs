using BookingTour.Business.Service.IService;
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
