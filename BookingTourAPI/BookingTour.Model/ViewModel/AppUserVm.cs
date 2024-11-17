using BookingTour.Model.Enum;

namespace BookingTour.Model.ViewModel
{
    public class AppUserVm
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string Roles { get; set; }
        public bool Status { get; set; }
        public List<UserBooking> Bookings { get; set; }
    }

    public class UserBooking
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public StatusType Status { get; set; }
        public int TourId { get; set; }
        public string Notes { get; set; }
    }
}
