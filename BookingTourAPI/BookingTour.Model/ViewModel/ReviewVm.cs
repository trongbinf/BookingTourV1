using BookingTour.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTour.Model.ViewModel
{
	public class ReviewVm
	{
		public int TourId { get; set; }
		public string UserId { get; set; }
		public int Rating { get; set; }
		public string? Comment { get; set; }
		public int BookingId { get; set; }
		public DateTime ReviewDate { get; set; } = DateTime.Now;
		public StatusType Status { get; set; }
	}
}
