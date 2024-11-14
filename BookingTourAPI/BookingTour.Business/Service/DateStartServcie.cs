using BookingTour.Business.Service.IService;
using BookingTour.Data.Repository.IRepository;
using BookingTour.Model;
using System.Linq.Expressions;

namespace BookingTour.Business.Service
{
	public class DateStartService : IDataStartService
	{
		private readonly IUnitOfWork _unitOfWork;

		public DateStartService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		
		public async Task AddAsync(DateStart entity)
		{
			await _unitOfWork.DateStart.AddAsync(entity);
			await _unitOfWork.SaveAsync(); 
		}

		
		public async Task DeleteAsync(int id)
		{
			var dateStart = await _unitOfWork.DateStart.GetFirstOrDefaultAsync(ds => ds.DateStartId == id);
			if (dateStart != null)
			{
				_unitOfWork.DateStart.Remove(dateStart);
				await _unitOfWork.SaveAsync();
			}
		}

		
		public async Task<IEnumerable<DateStart>> GetAllAsync(Expression<Func<DateStart, bool>>? filter = null, string? includeProperties = null)
		{
			return await _unitOfWork.DateStart.GetAllAsync(filter, includeProperties);
		}

	
		public async Task<DateStart> GetByIdAsync(int id)
		{
			return await _unitOfWork.DateStart.GetFirstOrDefaultAsync(ds => ds.DateStartId == id);
		}

	
		public async Task UpdateAsync(DateStart entity)
		{
			_unitOfWork.DateStart.UpdateAsync(entity); 
			await _unitOfWork.SaveAsync(); 
		}

		public async Task<DateStart> GetFirstOrDefaultAsync(Expression<Func<DateStart, bool>> filter, string? includeProperties = null)
		{

			return await _unitOfWork.DateStart.GetFirstOrDefaultAsync(filter, includeProperties);
		}

	}
}
