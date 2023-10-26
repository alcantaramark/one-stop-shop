using Microsoft.Identity.Client;
using one_stop_shop.datacontext;
using one_stop_shop.repository.interfaces;

namespace one_stop_shop.repository;

public class UnitOfWork: IUnitOfWork
{
    #region Private Members
    private readonly DataContext _context;
    private UserRepository _userRepository;
    #endregion

    #region Constructors
    public UnitOfWork(DataContext context)
    {
        _context = context;
    }
    #endregion

    #region Implementations
    public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
    
    #endregion

    #region Public Methods
    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }
    
    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }
    #endregion
}
