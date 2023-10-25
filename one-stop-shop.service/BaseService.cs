using one_stop_shop.service.interfaces;
using one_stop_shop.repository;
using System.Linq.Expressions;
using AutoMapper;
using one_stop_shop.repository.interfaces;
using one_stop_shop.service.configurations;

namespace one_stop_shop.service;

public abstract class BaseService<M, T, E> : IBaseService<M, T, E>
    where E : BaseRepository<T>
    where T : class
{
    #region Private Members
    E _entity;
    protected readonly IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    #endregion

    #region Constructors
    public BaseService(IUnitOfWork unitOfWork, IBaseRepository<T> obj)
    {
        _unitOfWork = unitOfWork;
        _mapper = new ServiceAutoMapperConfiguration().Configure().CreateMapper();
        _entity = (E)obj;
    }
    #endregion

    #region Implementations
    public async Task AddAsync(M model)
    {
        T item = _mapper.Map<T>(model);
        await _entity.AddAsync(item);
    }

    public async Task AddAsync(IEnumerable<M> models)
    {
        IEnumerable<T> items = _mapper.Map<IEnumerable<T>>(models);
        await _entity.AddAsync(items);
    }

    public async Task<IEnumerable<M>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<M>>(await _entity.GetAllAsync());
    }

    public async Task<M> GetAsync(Guid id)
    {
        return _mapper.Map<M>(await _entity.GetAsync(id));
    }

    public async Task<IEnumerable<M>> GetAsync(Expression<Func<T, bool>> where, int? page = null, int? pageSize = null)
    {
        return _mapper.Map<IEnumerable<M>>(await _entity.GetAsync(where, page, pageSize));
    }

    public async Task<IEnumerable<M>> GetAsync(int? page = null, int? pageSize = null)
    {
        return _mapper.Map<IEnumerable<M>>(await _entity.GetAsync(page, pageSize));
    }

    public async Task<M> GetAsync(Expression<Func<T, bool>> where)
    {
        return _mapper.Map<M>(await _entity.GetAsync(where));
    }

    public async Task RemoveAsync(Guid id)
    {
        await _entity.RemoveAsync(id);
    }

    public async Task RemoveAsync(IEnumerable<M> models)
    {
        await _entity.RemoveAsync(_mapper.Map<T>(models));
    }

    public async Task RemoveAsync(M model, params Expression<Func<T, object>>[] includedProperties)
    {
        await _entity.RemoveAsync(_mapper.Map<T>(model), includedProperties);
    }

    public async Task UpdateAsync(M model)
    {
        await _entity.UpdateAsync(_mapper.Map<T>(model));
    }
    #endregion
}
