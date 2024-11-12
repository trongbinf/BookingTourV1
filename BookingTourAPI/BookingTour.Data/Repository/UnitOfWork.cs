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
	public class UnitOfWork : IUnitOfWork
	{
		private readonly BookingTourDbContext _db;
		public ICategory Category { get; private set; }
		public IBooking Booking { get; private set; }
		public IActivity Activity { get; private set; }
		public IDateStart DateStart { get; private set; }
		public IReview Review { get; private set; }
		public ITour Tour { get; private set; }

		public UnitOfWork(BookingTourDbContext db)
		{
			_db = db;
			Category = new CategoryRepository(_db);
			Booking = new BookingRepository(_db);
			Activity = new ActivityRepository(_db);
			DateStart = new DateStartRepository(_db);
			Review = new ReviewRepository(_db);
			Tour = new TourRepository(_db);
		}
		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}

		public void Dispose()
		{
			_db.Dispose();
		}

	}
}
