using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using one_stop_shop.repository.interfaces;
using one_stop_shop.datacontext;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Runtime.InteropServices;

namespace one_stop_shop.repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    #region Proctected Members
    protected readonly DbContext _context;
    #endregion

    #region Constructor
    public BaseRepository(DbContext context)
    {
        _context = context;
    }
    #endregion
    
    #region Implementations
    public async Task AddAsync(T model)
    {
        try
        {
            await _context.Set<T>().AddAsync(model);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task AddAsync(IEnumerable<T> models)
    {
        try
        {
            await _context.Set<T>().AddRangeAsync(models);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
            return await Task.Run(() => _context.Set<T>().AsEnumerable<T>());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<T> GetAsync(Guid id)
    {
        try
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity is not null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            
            return entity;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> where)
    {
        try
        {
            var entity = await _context.Set<T>().Where(where).FirstOrDefaultAsync();

            if (entity is not null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> where, int? page = null, int? pageSize = null)
    {
        try
        {
            var query = await _context.Set<T>().Where(where).ToListAsync();
                
            if (page.HasValue && pageSize.HasValue)
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).AsEnumerable<T>();
            else
                return query.AsEnumerable<T>();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IEnumerable<T>> GetAsync(int? page = null, int? pageSize = null)
    {
        try
        { 
            var query = await _context.Set<T>().ToListAsync();
                
            if (page.HasValue && pageSize.HasValue)
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).AsEnumerable<T>();
            else
                return query.AsEnumerable<T>();            
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
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task RemoveAsync(IEnumerable<T> models)
    {
        try
        {
            await Task.Run(async () =>  
            { 
                _context.Set<T>().RemoveRange(models);
                await _context.SaveChangesAsync();
            });
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task RemoveAsync(T model, params Expression<Func<T, object>>[] includeProperties)
    {
        try
        {
            await Task.Run(async () =>
            {
                _context.Set<T>().Attach(model);
                var dbEntry = _context.Entry(model);
                
                foreach (var includedProperty in includeProperties)
                {
                    dbEntry.Property(includedProperty).IsModified = true;
                }
                await _context.SaveChangesAsync();
            });
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task UpdateAsync(T model)
    {
        try
        {
            await Task.Run(async () => 
            {
                _context.Set<T>();
                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            });
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}
