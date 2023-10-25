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
    public void Dispose()
    {
        _context.Dispose();
    }
    #endregion
}
