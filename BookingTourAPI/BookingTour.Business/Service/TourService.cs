using BookingTour.Business.Service.IService;
using BookingTour.Model;
using BookingTour.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

		
		public async Task<Tour> GetByIdAsync(int id)
		{
			return await _unitOfWork.Tour.GetFirstOrDefaultAsync(t => t.TourId == id);
		}

	
		public async Task UpdateAsync(Tour entity)
		{
			_unitOfWork.Tour.UpdateAsync(entity); 
			await _unitOfWork.SaveAsync(); 
		}
	}
}
