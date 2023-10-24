using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace one_stop_shop.repository;

public interface IBaseRepository<T> where T: class
{
    #region Contract Methods
    Task<T> GetAsync(Guid id);
    Task<T> GetAsync(Expression<Func<T, bool>> where);
    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> where, int? page = 1, int pageSize = 10);
    Task<IEnumerable<T>> GetAsync(int? page = 1, int pageSize = 10);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T model);
    Task AddAsync(IEnumerable<T> models);
    Task UpdateAsync(T model);
    Task RemoveAsync(Guid id);
    Task RemoveAsync(IEnumerable<T> models);
    Task RemoveAsync(T model, params Expression<Func<T, object>>[] includeProperties);
    #endregion
}
