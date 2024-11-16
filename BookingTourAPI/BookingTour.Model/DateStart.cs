using BookingTour.Model.Enum;

namespace BookingTour.Model
{
    public class DateStart
	{
        public int DateStartId {  get; set; }
        public DateTime StartDate {get;set;}
        public StatusType TypeStatus { get; set; }

		public int TourId { get; set; }
		public  Tour Tour { get; set; }
	}
}