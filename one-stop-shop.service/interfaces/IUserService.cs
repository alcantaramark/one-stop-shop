using one_stop_shop.dto;
using one_stop_shop.model;
using one_stop_shop.repository.interfaces;
using one_stop_shop.service.interfaces;

namespace one_stop_shop.service;

public interface IUserService: IBaseService<UserDto, User, IUserRepository>
{

}
