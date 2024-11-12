using BookingTour.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace BookingTour.Model
{
 
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }
        public ActivityType ActivityType { get; set; }
        public string ActivityName { get; set; }
        public string Description {  get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
