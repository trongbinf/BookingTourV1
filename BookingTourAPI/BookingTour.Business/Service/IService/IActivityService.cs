using BookingTour.Model;

namespace BookingTour.Business.Service.IService
{
	public interface IActivityService : IBaseService<Activity> {
        TimeSpan Handle(string input);

    }
}
