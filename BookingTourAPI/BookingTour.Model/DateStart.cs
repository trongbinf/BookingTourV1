using BookingTour.Model.Enum;
using System.Text.Json.Serialization;

namespace BookingTour.Model
{
    public class DateStart
	{
        public int DateStartId {  get; set; }
        public DateTime StartDate {get;set;}
        public StatusType TypeStatus { get; set; }
<<<<<<< HEAD

		public int TourId { get; set; }
		public  Tour Tour { get; set; }
=======
        public int TourId { get; set; }
        [JsonIgnore]
		public Tour Tour { get; set; }
>>>>>>> 9a8931b9eff9d1b1757b58f0dc48cc13e59b58b5
	}
}