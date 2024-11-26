using BookingTour.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookingTour.Model
{

	public class Activity
	{
		[Key]
		public int ActivityId { get; set; }
		public TimeSpan Duration { get; set; }
		public ActivityType ActivityType { get; set; }
		public string ActivityName { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
		public int TourId { get; set; }     
        public virtual Tour Tour {  get; set; }
	}
}
