using BookingTour.Model.Enum;

namespace BookingTour.Model.ViewModel
{
    public class ActivityVm
    {
        public string Duration { get; set; }
        public ActivityType ActivityType { get; set; }
        public string ActivityName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int TourId { get; set; }
    }
}
