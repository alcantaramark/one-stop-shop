﻿using System.Threading.Tasks;

namespace one_stop_shop.repository.interfaces;

public interface IUnitOfWork
{
    #region Data Access
    IUserRepository UserRepository { get; }
    #endregion

    #region Contract Methods
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task DisposeAsync();
    Task RollbackTransactionAsync();
    #endregion
}
