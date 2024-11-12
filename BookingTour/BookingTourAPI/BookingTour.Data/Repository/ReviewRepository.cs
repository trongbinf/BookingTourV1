using BookingTour.Data.Data;
using BookingTour.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTour.Data.Repository.IRepository
{
	public class ReviewRepository : Repository<Review>, IReview
	{
		private readonly BookingTourDbContext _db;
		public ReviewRepository(BookingTourDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task UpdateAsync(Review review)
		{
			_db.Reviews.Update(review);
		}


	}
}
