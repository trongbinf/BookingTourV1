using BookingTour.Business.Service;
using BookingTour.Business.Service.IService;
using BookingTour.Model;
using BookingTour.Model.Enum;
using BookingTour.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingTour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
		private readonly IReviewService _reviewService;
		private readonly ITourService _tourService;
		private readonly UserManager<AppUser> _userManager;

		public ReviewController(IReviewService reviewService,
			                   UserManager<AppUser> userManager,
								ITourService tourService)
		{
			_reviewService = reviewService;
			_userManager = userManager;
			_tourService = tourService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
		{
			var reviews = await _reviewService.GetAllAsync(includeProperties: "Tour, User");
			return Ok(reviews);
		}

		[HttpGet("reviews-by-tour/{tourId}")]
		public async Task<ActionResult<IEnumerable<Review>>> GetAllReviewsByTourId(int tourId)
		{
			var reviews = await _reviewService.GetAllAsync(x => x.TourId == tourId, includeProperties: "Tour, User");

			if (reviews == null || !reviews.Any())
			{
				return NotFound(new { message = "No reviews found for the specified tour." });
			}

			return Ok(reviews);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<IEnumerable<Review>>> GetAllReviewsById(int id)
		{
			var reviews = await _reviewService.GetFirstOrDefaultAsync(x => x.ReviewId == id, includeProperties: "Tour, User");
			return Ok(reviews);
		}


		[HttpPost("add")]
		
		public async Task<IActionResult> AddReview([FromBody] ReviewVm reviewVm)
		{
			if (reviewVm == null)
			{
				return BadRequest(new { message = "Dữ liệu đánh giá không hợp lệ." });
			}

			var tour = await _tourService.GetFirstOrDefaultAsync(x => x.TourId == reviewVm.TourId);
			if (tour == null)
			{
				return NotFound(new { message = "Không tìm thấy tour tương ứng." });
			}

			var user = await _userManager.FindByIdAsync(reviewVm.UserId);
			if (user == null)
			{
				return NotFound(new { message = "Không tìm thấy người dùng tương ứng." });
			}

		
			var review = new Review
			{
				TourId = reviewVm.TourId,
				UserId = reviewVm.UserId,
				Rating = reviewVm.Rating,
				Comment = reviewVm.Comment,
				ReviewDate = reviewVm.ReviewDate,
				Status =reviewVm.Status,
				BookingId = reviewVm.BookingId
			    
			};

			try
			{
				await _reviewService.AddAsync(review);
				return Ok(new { message = "Đánh giá đã được thêm thành công." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Đã xảy ra lỗi khi thêm đánh giá.", error = ex.Message });
			}
		}

		[HttpPut("set-status/{id}")]
		
		public async Task<IActionResult> SetReviewStatus(int id, int status)
		{		
			if (status != (int)StatusType.Available && status != (int)StatusType.Unavailable)
			{
				return BadRequest("Invalid status. Only 'Available' or 'Unavailable' are allowed.");
			}	
			var review = await _reviewService.GetFirstOrDefaultAsync(x => x.ReviewId == id);
			if (review == null)
			{
				return NotFound("Review not found.");
			}			
			review.Status = (StatusType)status;

			try
			{			
				await _reviewService.UpdateAsync(review);
				string statusMessage = status == (int)StatusType.Available ? "Available" : "Unavailable";
				return Ok(new { message = $"Review status has been updated to {statusMessage}." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "An error occurred while updating the status.", error = ex.Message });
			}
		}

		[HttpGet("total-rating")]
		public async Task<ActionResult<object>> GetTotalRatingByTourId(int tourId)
		{		
			var reviews = await _reviewService.GetAllAsync(x => x.TourId == tourId);
		
			if (reviews == null || !reviews.Any())
			{
				return NotFound(new { message = "No reviews found for the specified tour." });
			}

	
			int totalRating = reviews.Sum(r => r.Rating);

			int totalReviewsCount = reviews.Count();

			return Ok(new { totalReviewsCount , totalRating });
		}


		[HttpGet("average-rating")]
		public async Task<ActionResult<double>> GetAverageRatingByTourId(int tourId)
		{
			
			var reviews = await _reviewService.GetAllAsync(x => x.TourId == tourId);

			if (reviews == null || !reviews.Any())
			{
				return NotFound(new { message = "No reviews found for the specified tour." });
			}

		
			double averageRating = reviews.Average(r => r.Rating);

			return Ok(new { averageRating });
		}

		[HttpGet("check-reviewed/{bookingId}")]
		public async Task<IActionResult> CheckIfBookingReviewed(int bookingId)
		{
			// Kiểm tra xem Booking có tồn tại không
			var booking = await _reviewService.GetFirstOrDefaultAsync(x => x.BookingId == bookingId);
			if (booking == null)
			{
				return NotFound(new { message = "Không tìm thấy thông tin booking hoặc booking chưa hoàn thành." });
			}

			// Kiểm tra xem Booking đã được đánh giá chưa
			var existingReview = await _reviewService.GetFirstOrDefaultAsync(x => x.BookingId == bookingId);
			if (existingReview != null)
			{
				return Ok(true); // Trả về true nếu đã được đánh giá
			}

			return Ok(false); // Trả về false nếu chưa được đánh giá
		}


	}
}
