using BookingTour.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingTour.Data.Data
{
    public class BookingTourDbContext : IdentityDbContext
	{
        public BookingTourDbContext(DbContextOptions<BookingTourDbContext> options)
            : base(options) { }

		public DbSet<RefreshToken> RefreshTokens { get; set; }
		public DbSet<Tour> Tours { get; set; }
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<DateStart> DateStarts { get; set; }
		public DbSet<Activity> Activities { get; set; }
        public DbSet<TourActivity> TourActivities { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
			builder.Entity<TourActivity>()
				.HasKey(ta => new {ta.TourId,ta.ActivityId});
			DataSend.InsertData(builder);
            base.OnModelCreating(builder);
        }

    }
}
