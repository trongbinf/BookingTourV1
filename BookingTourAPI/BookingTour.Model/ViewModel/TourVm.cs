using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingTour.Model.ViewModel
{
	public class TourVm
	{
		public Tour Tour { get; set; } 

		public IEnumerable<DateStart> DateStarts { get; set; } 
		public IEnumerable<TourActivity> TourActivities { get; set; } 
		public IEnumerable<Booking> Bookings { get; set; } 
		public IEnumerable<Review> Reviews { get; set; } 
		public Category Category { get; set; } 	
		public IFormFile? MainImage { get; set; }
		public List<IFormFile>? DetailImages { get; set; }
	}
}
