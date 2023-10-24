using System.Threading.Tasks;

namespace one_stop_shop.repository.interfaces;

public interface IUnitOfWork
{
    #region Data Access
    IUserRepository UserRepository { get; }
    Task<int> CommitAsync();
    #endregion
}
