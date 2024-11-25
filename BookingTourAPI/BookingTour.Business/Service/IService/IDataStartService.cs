using BookingTour.Data.Repository.IRepository;
using BookingTour.Model;

namespace BookingTour.Business.Service.IService
{
	public interface IDataStartService : IBaseService<DateStart>
	{
        DateOnly ConvertToDateOnlyArray(string dateStrings);

    }
}
