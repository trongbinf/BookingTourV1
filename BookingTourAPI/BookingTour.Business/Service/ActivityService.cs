using BookingTour.Business.Service.IService;
using BookingTour.Data.Repository.IRepository;
using BookingTour.Model;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookingTour.Business.Service
{
	public class ActivityService : IActivityService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ActivityService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task AddAsync(Activity entity)
		{
			await _unitOfWork.Activity.AddAsync(entity);
			await _unitOfWork.SaveAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _unitOfWork.Activity.GetFirstOrDefaultAsync(x => x.ActivityId == id);
			if (entity != null)
			{
				_unitOfWork.Activity.Remove(entity);
				await _unitOfWork.SaveAsync();
			}
		}

		public async Task<IEnumerable<Activity>> GetAllAsync(Expression<Func<Activity, bool>>? filter = null, string? includeProperties = null)
		{
			return await _unitOfWork.Activity.GetAllAsync(filter, includeProperties);
		}

		public async Task<Activity> GetByIdAsync(int id)
		{
			return await _unitOfWork.Activity.GetFirstOrDefaultAsync(x => x.ActivityId == id);
		}

		public async Task UpdateAsync(Activity entity)
		{
		
			_unitOfWork.Activity.UpdateAsync(entity);
			await _unitOfWork.SaveAsync();
		}
		public async Task<Activity> GetFirstOrDefaultAsync(Expression<Func<Activity, bool>> filter, string? includeProperties = null)
		{

			return await _unitOfWork.Activity.GetFirstOrDefaultAsync(filter, includeProperties);
		}

	}
}
