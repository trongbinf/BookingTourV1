using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTour.Data.Repository.IRepository
{
	public interface IUnitOfWork : IDisposable
	{
		ICategory Category { get; }
		IBooking Booking { get; }
		IActivity Activity { get; }
		IDateStart DateStart { get; }
		IReview Review { get; }
		ITour Tour { get; }
		Task SaveAsync();
	}
}
