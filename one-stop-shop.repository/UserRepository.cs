using one_stop_shop.datacontext;
using one_stop_shop.model;
using one_stop_shop.repository.interfaces;

namespace one_stop_shop.repository;

public class UserRepository: BaseRepository<User>, IUserRepository
{
    #region Private Members
    private readonly DataContext _context;
    #endregion

    #region Constructors
    public UserRepository(DataContext context): base(context)
    {
        _context = context;
    }
    #endregion
}
