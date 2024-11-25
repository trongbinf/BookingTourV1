using BookingTour.Model.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookingTour.Model
{
	public class Review
	{
		public int ReviewId { get; set; }

		public int BookingId { get; set; } 
		public int TourId { get; set; }
		public string UserId { get; set; }
		public int Rating { get; set; }
		public string? Comment { get; set; }
		public DateTime ReviewDate { get; set; } = DateTime.Now;
		public StatusType Status { get; set; }
		
		public virtual Tour Tour { get; set; }
		
		public virtual AppUser User { get; set; }
	
		public virtual Booking Booking { get; set; }

	}
}
