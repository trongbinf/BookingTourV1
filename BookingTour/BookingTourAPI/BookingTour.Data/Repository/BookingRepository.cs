using BookingTour.Data.Data;
using BookingTour.Data.Repository.IRepository;
using BookingTour.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTour.Data.Repository
{
	public class BookingRepository : Repository<Booking>, IBooking
	{
		private readonly BookingTourDbContext _db;
		public BookingRepository(BookingTourDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task UpdateAsync(Booking booking)
		{
			_db.Bookings.Update(booking);
		}

	}
}
