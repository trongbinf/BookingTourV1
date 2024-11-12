using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebGameV1.DataAcess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
    
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);    
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task AddAsync(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
	}
}
