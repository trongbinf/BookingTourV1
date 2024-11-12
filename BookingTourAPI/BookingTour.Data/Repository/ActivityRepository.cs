using BookingTour.Data.Data;
using BookingTour.Data.Repository.IRepository;
using BookingTour.Model;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTour.Data.Repository
{
	public class ActivityRepository : Repository<Activity>, IActivity
	{
		private readonly BookingTourDbContext _db;

		public ActivityRepository(BookingTourDbContext db) : base(db)
		{
			_db = db;
		}


		public async Task UpdateAsync(Activity activity)
		{
			_db.Activities.Update(activity);
		}
	}
}
