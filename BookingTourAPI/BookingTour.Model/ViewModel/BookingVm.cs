using BookingTour.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTour.Model.ViewModel
{
	public class BookingVm
	{
		public DateTime BookingDate { get; set; }
		public DateTime PickDate { get; set; }
		public string StartTime { get; set; }
		public int PersonNumber { get; set; }
		public string? Notes { get; set; }
		public StatusType Status { get; set; }
     	public int TourId { get; set; }
		public string UserId { get; set; }

	}
}
