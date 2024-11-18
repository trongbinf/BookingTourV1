
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;


namespace BookingTour.Model
{
	public class AppUser : IdentityUser
	{
		public string FullName { get; set; }
		public List<RefreshToken> RefreshTokens { get; set; }

		[JsonIgnore]
		public virtual ICollection<Booking> Bookings { get; set; } 
	}
}
