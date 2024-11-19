using Microsoft.AspNetCore.Http;

namespace BookingTour.Model.ViewModel
{
	public class TourVm
	{
		public Tour Tour { get; set; }

		public IEnumerable<DateStart> DateStarts { get; set; }	
		public IEnumerable<Activity> Activities { get; set; }
		public IEnumerable<Booking> Bookings { get; set; }
		public IEnumerable<Review> Reviews { get; set; }
		public Category Category { get; set; }
		public IFormFile? MainImage { get; set; }
		public List<IFormFile>? DetailImages { get; set; }
	}
}
