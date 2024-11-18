using BookingTour.Business.Service.IService;
using BookingTour.Data.Repository.IRepository;
using BookingTour.Model;
using System.Linq.Expressions;
using rm = BookingTour.Business.Service.HandleTextUnicode;
namespace BookingTour.Business.Service
{
    public class TourService : ITourService
	{
		private readonly IUnitOfWork _unitOfWork;

		public TourService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		
		public async Task AddAsync(Tour entity)
		{
			await _unitOfWork.Tour.AddAsync(entity);
			await _unitOfWork.SaveAsync(); 
		}

	
		public async Task DeleteAsync(int id)
		{
			var tour = await _unitOfWork.Tour.GetFirstOrDefaultAsync(t => t.TourId == id);
			if (tour != null)
			{
				_unitOfWork.Tour.Remove(tour);
				await _unitOfWork.SaveAsync(); 
			}
		}

	
		public async Task<IEnumerable<Tour>> GetAllAsync(Expression<Func<Tour, bool>>? filter = null, string? includeProperties = null)
		{
			return await _unitOfWork.Tour.GetAllAsync(filter, includeProperties);
		}


        public async Task<IEnumerable<string>> GetAllCountry()
        {
			var list = await _unitOfWork.Tour.GetAllCountry();
			return list;
		}
        public async Task<IEnumerable<string>> GetAllCityByCountry(string country)
        {
			var handleText = rm.RemoveUnicode(country);
			var list = await _unitOfWork.Tour.GetAllCityByCountry(handleText);
			return list;
		}

        public async Task<Tour> GetByIdAsync(int id)
		{
			return await _unitOfWork.Tour.GetFirstOrDefaultAsync(t => t.TourId == id);

	
		}

		public async Task<Tour> GetFirstOrDefaultAsync(Expression<Func<Tour, bool>> filter, string? includeProperties = null)
		{
		
			return await _unitOfWork.Tour.GetFirstOrDefaultAsync(filter, includeProperties);
		}

		public async Task UpdateAsync(Tour entity)
		{
			_unitOfWork.Tour.UpdateAsync(entity); 
			await _unitOfWork.SaveAsync(); 
		}
	}
}
