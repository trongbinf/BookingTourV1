using BookingTour.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGameV1.DataAcess.Repository.IRepository;

namespace BookingTour.Data.Repository.IRepository
{
	public interface ITour : IRepository<Tour>
	{
		Task UpdateAsync(Tour tour);
	
	}
}
