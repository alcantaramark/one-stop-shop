using System.Linq.Expressions;
using System.Threading.Tasks;

namespace one_stop_shop.service.interfaces;

public interface IBaseService<M, T, E>
{
    #region Contact Methods
    Task<M> GetAsync(Guid id);
    Task<IEnumerable<M>> GetAsync(Expression<Func<T, bool>> where, int? page = null, int? pageSize = null);
    Task<IEnumerable<M>> GetAsync(int? page = null, int? pageSize = null);
    Task<M>GetAsync(Expression<Func<T, bool>> where);
    Task<IEnumerable<M>> GetAllAsync();
    Task AddAsync(M model);
    Task AddAsync(IEnumerable<M> models);
    Task UpdateAsync(M model);
    Task RemoveAsync(Guid id);
    Task RemoveAsync(IEnumerable<M> models);
    Task RemoveAsync(M model, params Expression<Func<T, object>>[] includedProperties);
    Task CommitAsync();
    Task RollBackTransactionAsync();
    Task BeginTransactionAsync();
    Task DisposeAsync();
    #endregion
}
