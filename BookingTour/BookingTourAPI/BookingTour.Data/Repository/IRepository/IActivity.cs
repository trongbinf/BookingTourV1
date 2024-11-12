using BookingTour.Model;
using WebGameV1.DataAcess.Repository.IRepository;

namespace BookingTour.Data.Repository.IRepository
{
	public interface IActivity: IRepository<Activity>
	{
		Task UpdateAsync(Activity activity);
	}
}
