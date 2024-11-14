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
	public class CategoryService : ICategoryService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoryService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

	
		public async Task AddAsync(Category entity)
		{
			await _unitOfWork.Category.AddAsync(entity);
			await _unitOfWork.SaveAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var category = await _unitOfWork.Category.GetFirstOrDefaultAsync(c => c.CategoryId == id);
			if (category != null)
			{
				_unitOfWork.Category.Remove(category);
				await _unitOfWork.SaveAsync();
			}
		}

		public async Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>>? filter = null, string? includeProperties = null)
		{
			return await _unitOfWork.Category.GetAllAsync(filter, includeProperties);
		}

		
		public async Task<Category> GetByIdAsync(int id)
		{
			return await _unitOfWork.Category.GetFirstOrDefaultAsync(c => c.CategoryId == id);
		}

		
		public async Task UpdateAsync(Category entity)
		{
			_unitOfWork.Category.UpdateAsync(entity);
			await _unitOfWork.SaveAsync();
		}
		public async Task<Category> GetFirstOrDefaultAsync(Expression<Func<Category, bool>> filter, string? includeProperties = null)
		{

			return await _unitOfWork.Category.GetFirstOrDefaultAsync(filter, includeProperties);
		}
	}
}
