
using System.ComponentModel.DataAnnotations.Schema;


namespace BookingTour.Model
{
	public class Review
	{
		public int ReviewId { get; set; }
		public int TourId { get; set; }
		public string UserId { get; set; }
		public int Rating { get; set; }
		public string? Comment { get; set; }
		public DateTime ReviewDate { get; set; } = DateTime.Now;
		public Tour Tour { get; set; }
		public AppUser User { get; set; }
	}
}
