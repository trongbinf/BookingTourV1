using BookingTour.Business.Service.IService;
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
                includeProperties: "Category,Reviews,TourActivities,Bookings,DateStarts"
            );

            var tourVms = tours.Select(tour => new TourVm
            {
                Tour = tour,
                DateStarts = tour.DateStarts,
                Category = tour.Category,
                Reviews = tour.Reviews,
                TourActivities = tour.TourActivities,
                Bookings = tour.Bookings
            });

            return Ok(tourVms);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryVm>>> GetCategoriesWithTours()
        {
            // Lấy toàn bộ dữ liệu các tour bao gồm category và các thuộc tính liên quan
            var tours = await _tourService.GetAllAsync(
                includeProperties: "Category,DateStarts,Reviews,TourActivities,Bookings"
            );

            // Nhóm các tour theo category và lấy 5 tour mỗi nhóm
            var groupedCategories = tours
                .GroupBy(t => t.Category)
                .Select(g => new CategoryVm
                {
                    Category = g.Key,
                    Tours = g.Take(5).ToList()
                }).ToList();

            return Ok(groupedCategories);
        }

        [HttpGet("categories/{categoryName}")]
        public async Task<ActionResult<IEnumerable<TourVm>>> GetToursByCategory(string categoryName)
        {
            string normalName = rm.RemoveUnicode(categoryName);
            var tours = await _tourService.GetAllAsync(
                filter: t => t.Category.Name == categoryName,
                includeProperties: "Category,DateStarts,Reviews,TourActivities,Bookings"
            );

            // Lấy tối đa 5 tour theo category đã chọn
            var tourVms = tours.Take(6).Select(tour => new TourVm
            {
                Tour = tour,
                DateStarts = tour.DateStarts,
                Category = tour.Category,
                Reviews = tour.Reviews,
                TourActivities = tour.TourActivities,
                Bookings = tour.Bookings
            }).ToList();

            return Ok(tourVms);
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
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 5)
        {
            var listTour = await _tourService.GetAllAsync(
                includeProperties: "Category,Reviews,TourActivities,Bookings,DateStarts");

            listTour = listTour.Where(t => t.Status == true).ToList();
            if (!string.IsNullOrEmpty(name))
            {
                var normalizedName = rm.RemoveUnicode(name);
                listTour = listTour.Where(t =>rm.RemoveUnicode(t.TourName).Contains(normalizedName, StringComparison.OrdinalIgnoreCase)).ToList();
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

            int totalCount = listTour.Count();

            var resultFindding = listTour.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            var resultOutput = resultFindding.Select(t => new TourVm
            {
                Tour = t,
                Bookings = t.Bookings,
                Category = t.Category,
                DateStarts = t.DateStarts,
                TourActivities = t.TourActivities,
                Reviews = t.Reviews,
                MainImage = null,
                DetailImages = null
            }).ToList();


            var response = new PaginatedList<TourVm>(resultOutput, totalCount, pageIndex, pageSize);
            return Ok(response);
        }

       


        [HttpGet("{id}")]
        public async Task<ActionResult<TourVm>> GetTourById(int id)
        {
            var tour = await _tourService.GetFirstOrDefaultAsync(x => x.TourId == id,
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
