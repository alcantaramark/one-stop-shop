using AutoMapper;
using one_stop_shop.model;
using one_stop_shop.dto;

namespace one_stop_shop.service.configurations;

public class ServiceClientMappingProfile: Profile
{
    #region Constructors
    public ServiceClientMappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
    #endregion
}
