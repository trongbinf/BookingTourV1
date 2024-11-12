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
	public class ReviewService : IReviewService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ReviewService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		
		public async Task AddAsync(Review entity)
		{
			await _unitOfWork.Review.AddAsync(entity);
			await _unitOfWork.SaveAsync(); 
		}

		
		public async Task DeleteAsync(int id)
		{
			var review = await _unitOfWork.Review.GetFirstOrDefaultAsync(r => r.ReviewId == id);
			if (review != null)
			{
				_unitOfWork.Review.Remove(review);
				await _unitOfWork.SaveAsync();
			}
		}

		public async Task<IEnumerable<Review>> GetAllAsync(Expression<Func<Review, bool>>? filter = null, string? includeProperties = null)
		{
			return await _unitOfWork.Review.GetAllAsync(filter, includeProperties);
		}

	
		public async Task<Review> GetByIdAsync(int id)
		{
			return await _unitOfWork.Review.GetFirstOrDefaultAsync(r => r.ReviewId == id);
		}


		public async Task UpdateAsync(Review entity)
		{
			_unitOfWork.Review.UpdateAsync(entity); 
			await _unitOfWork.SaveAsync(); 
		}
	}
}
