using AutoMapper;
using one_stop_shop.datacontext;
using one_stop_shop.dto;
using one_stop_shop.model;
using one_stop_shop.repository;
using one_stop_shop.repository.interfaces;
using one_stop_shop.service.configurations;

namespace one_stop_shop.service;

public class UserService: BaseService<UserDto, User, UserRepository>, IUserService
{
    #region Private Members
    private readonly IUnitOfWork _unitOfWork;
    private DataContext _context;
    private IMapper _mapper;
    #endregion

    #region Constructors
    public UserService(IUnitOfWork unitOfWork, DataContext context): base(unitOfWork, new UserRepository(context))
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = new ServiceAutoMapperConfiguration().Configure().CreateMapper();
    }
    #endregion
}
