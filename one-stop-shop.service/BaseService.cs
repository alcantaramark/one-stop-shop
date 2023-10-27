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
        try
        {
            T item = _mapper.Map<T>(model);
            await _entity.AddAsync(item);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task AddAsync(IEnumerable<M> models)
    {
        try
        {
            IEnumerable<T> items = _mapper.Map<IEnumerable<T>>(models);
            await _entity.AddAsync(items);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IEnumerable<M>> GetAllAsync()
    {
        try
        {
            return _mapper.Map<IEnumerable<M>>(await _entity.GetAllAsync());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<M> GetAsync(Guid id)
    {
        try
        {
            return _mapper.Map<M>(await _entity.GetAsync(id));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IEnumerable<M>> GetAsync(Expression<Func<T, bool>> where, int? page = null, int? pageSize = null)
    {
        try
        {
            return _mapper.Map<IEnumerable<M>>(await _entity.GetAsync(where, page, pageSize));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IEnumerable<M>> GetAsync(int? page = null, int? pageSize = null)
    {
        try
        {
            return _mapper.Map<IEnumerable<M>>(await _entity.GetAsync(page, pageSize));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<M> GetAsync(Expression<Func<T, bool>> where)
    {
        try
        {
            return _mapper.Map<M>(await _entity.GetAsync(where));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task RemoveAsync(Guid id)
    {
        try
        {
            await _entity.RemoveAsync(id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task RemoveAsync(IEnumerable<M> models)
    {
        try
        {
            await _entity.RemoveAsync(_mapper.Map<T>(models));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task RemoveAsync(M model, params Expression<Func<T, object>>[] includedProperties)
    {
        try
        {
            await _entity.RemoveAsync(_mapper.Map<T>(model), includedProperties);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task UpdateAsync(M model)
    {
        try
        {
            await _entity.UpdateAsync(_mapper.Map<T>(model));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task CommitAsync()
    {
        await _unitOfWork.CommitAsync();
    }

    public async Task RollBackTransactionAsync()
    {
        await _unitOfWork.RollbackTransactionAsync();
    }

    public async Task BeginTransactionAsync()
    {
        await _unitOfWork.BeginTransactionAsync();
    }

    public async Task DisposeAsync()
    {
        await _unitOfWork.DisposeAsync();
    }
    #endregion
}
