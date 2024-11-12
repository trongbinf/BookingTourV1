using BookingTour.Model;
using BookingTour.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookingTour.Business.Service.IService;

namespace BookingTour.Business.Service
{
	public class Bookingervice : IBookingService
	{
		private readonly IUnitOfWork _unitOfWork;

		public Bookingervice(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task AddAsync(Booking entity)
		{
			await _unitOfWork.Booking.AddAsync(entity);
			await _unitOfWork.SaveAsync(); // Commit changes
		}

		public async Task DeleteAsync(int id)
		{
			var booking = await _unitOfWork.Booking.GetFirstOrDefaultAsync(b => b.BookingId == id);
			if (booking != null)
			{
				_unitOfWork.Booking.Remove(booking);
				await _unitOfWork.SaveAsync(); // Commit changes
			}
		}

		public async Task<IEnumerable<Booking>> GetAllAsync(Expression<Func<Booking, bool>>? filter = null, string? includeProperties = null)
		{
			return await _unitOfWork.Booking.GetAllAsync(filter, includeProperties);
		}

		public async Task<Booking> GetByIdAsync(int id)
		{
			return await _unitOfWork.Booking.GetFirstOrDefaultAsync(b => b.BookingId == id);
		}

		public async Task UpdateAsync(Booking entity)
		{
			_unitOfWork.Booking.UpdateAsync(entity); 
			await _unitOfWork.SaveAsync(); 
		}
	}
}
