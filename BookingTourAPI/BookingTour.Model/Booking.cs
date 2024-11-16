
using System.ComponentModel.DataAnnotations.Schema;
using BookingTour.Model.Enum;


namespace BookingTour.Model
{
    public class Booking
	{
		public int BookingId { get; set; }	
		public DateTime BookingDate { get; set; } = DateTime.Now;
		public string? Notes { get; set; }
		public StatusType Status { get; set; }

		public int TourId { get; set; }
		public  Tour Tour { get; set; }
		public string UserId { get; set; }
		public AppUser User { get; set; }

	}
}
