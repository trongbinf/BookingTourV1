﻿using BookingTour.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

using WebGameV1.DataAcess.Repository.IRepository;

namespace BookingTour.Data.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly BookingTourDbContext _db;
		internal DbSet<T> DbSet;

		public Repository(BookingTourDbContext db)
		{
			_db = db;
			this.DbSet = _db.Set<T>();
		}

		public async Task AddAsync(T entity)
		{
			await DbSet.AddAsync(entity);
		}

		public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
		{
			IQueryable<T> query = DbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (includeProperties != null)
			{
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp.Trim());
				}
			}

			return await query.ToListAsync();
		}

		public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			IQueryable<T> query = DbSet;

			if (includeProperties != null)
			{
				foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(property.Trim());
				}
			}

			query = query.Where(filter);
			return await query.FirstOrDefaultAsync();
		}

		public void Remove(T entity)
		{
			DbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			DbSet.RemoveRange(entities);
		}
	}
}
