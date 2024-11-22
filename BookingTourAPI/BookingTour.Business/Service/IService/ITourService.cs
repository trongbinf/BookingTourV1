using BookingTour.Model;

namespace BookingTour.Business.Service.IService
{
    public interface  ITourService : IBaseService<Tour>
	{
        Task<IEnumerable<string>> GetAllCountry();
        Task<IEnumerable<string>> GetAllCityByCountry(string country);
       
    }
}
