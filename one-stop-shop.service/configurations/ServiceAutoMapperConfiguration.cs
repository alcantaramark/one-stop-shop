using AutoMapper;

namespace one_stop_shop.service.configurations;

public class ServiceAutoMapperConfiguration
{
    #region Methods
    public MapperConfiguration Configure()
    {
        return new MapperConfiguration(cfg => 
        {
            cfg.AddProfile<ServiceClientMappingProfile>();
        });
    }
    #endregion
}
