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
	public class DateStartRepository : Repository<DateStart>, IDateStart
	{

		private readonly BookingTourDbContext _db;
		public DateStartRepository(BookingTourDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task UpdateAsync(DateStart dateStart)
		{
			_db.DateStarts.Update(dateStart);
		}

	}
}
