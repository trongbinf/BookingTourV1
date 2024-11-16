using BookingTour.Data.Data;
using BookingTour.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTour.Data.Repository.IRepository
{
	public class TourRepository : Repository<Tour>, ITour
	{
		private readonly BookingTourDbContext _db;
		public TourRepository(BookingTourDbContext db) : base(db)
		{
			_db = db;
		}


		public async Task UpdateAsync(Tour tour)
		{
			var objFromDb = await _db.Tours.FindAsync(tour.TourId);
			if (objFromDb != null)
			{

				objFromDb.Description = tour.Description;
				objFromDb.Price = tour.Price;
				objFromDb.City = tour.City;
				objFromDb.Status = tour.Status;
				objFromDb.Country = tour.Country;
				objFromDb.IsFullDay = tour.IsFullDay;
				objFromDb.Price = tour.Price;
				objFromDb.PersonNumber = tour.PersonNumber;
				objFromDb.Status = tour.Status;

				if (!string.IsNullOrEmpty(tour.MainImage))
				{
					objFromDb.MainImage = tour.MainImage;
				}

				if (!string.IsNullOrEmpty(tour.OtherImage))
				{
					objFromDb.OtherImage = tour.OtherImage;
				}
			}


		}
		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}

	}
}
