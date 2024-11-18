using BookingTour.Model.Enum;
using System.Text.Json.Serialization;

namespace BookingTour.Model
{
    public class DateStart
	{
        public int DateStartId {  get; set; }
        public DateOnly StartDate {get;set;}
        public StatusType TypeStatus { get; set; }
        public int TourId { get; set; }
        [JsonIgnore]
		public Tour Tour { get; set; }
	}
}