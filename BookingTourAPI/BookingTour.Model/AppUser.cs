
using Microsoft.AspNetCore.Identity;


namespace BookingTour.Model
{
	public class AppUser : IdentityUser
	{
		public string FullName { get; set; }
		public List<RefreshToken> RefreshTokens { get; set; }

		public ICollection<Booking> Bookings { get; set; }   
	}
}
