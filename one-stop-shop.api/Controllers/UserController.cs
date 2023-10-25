using System.Net;
using Microsoft.AspNetCore.Mvc;
using one_stop_shop.service;

namespace one_stop_shop.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    #region Private Members
    private readonly IUserService _userService;
    #endregion

    #region Constructors
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    #endregion


    #region API Methods
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest("Error processing your request");
        }
    }
    #endregion
}
