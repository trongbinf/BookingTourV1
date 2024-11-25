
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BookingTour.Model.Enum;


namespace BookingTour.Model
{
    public class Booking
	{
		[Key]
		public int BookingId { get; set; }	
		public DateTime BookingDate { get; set; }
		public DateTime PickDate { get; set; }
		public TimeSpan StartTime { get; set; }
		public int PersonNumber { get; set; }
		public string? Notes { get; set; }
		public StatusType Status { get; set; }
	
		public int TourId { get; set; }
		public virtual Tour Tour { get; set; }
		public string UserId { get; set; }
		public virtual AppUser User { get; set; }

		[JsonIgnore]
		public virtual ICollection<Review> Reviews { get; set; }

	}
}
