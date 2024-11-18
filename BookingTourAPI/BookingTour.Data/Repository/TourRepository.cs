using BookingTour.Data.Data;
using BookingTour.Model;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<string>> GetAllCountry()
        {
			var list = await _db.Tours.Select(t => t.Country).Distinct().ToListAsync();
			return list;
		}

        public async Task<IEnumerable<string>> GetAllCityByCountry(string country)
        {
            var list = await _db.Tours.Where(t => (t.Country == country)).Select(t => t.City).Distinct().ToListAsync();
            return list;
        }
    }
}
