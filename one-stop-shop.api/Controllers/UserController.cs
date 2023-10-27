using System.Net;
using Microsoft.AspNetCore.Mvc;
using one_stop_shop.dto;
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
        finally
        {
            await _userService.DisposeAsync();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserDto user)
    {
        try
        {
            await _userService.BeginTransactionAsync();
            await _userService.AddAsync(user);
            await _userService.CommitAsync();
            return Ok("User added");
        }
        catch (Exception ex)
        {
            await _userService.RollBackTransactionAsync();
            return BadRequest("Error processing your request");
        }
        finally
        {
            await _userService.DisposeAsync();
        }
    }

    [HttpPut]
    [Route("/:id")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UserDto user)
    {
        try
        {
            var userEntity = await _userService.GetAsync(id);

            if (userEntity is null)
            {
                return NotFound("User not found");
            }

            userEntity.Name = user.Name;
            userEntity.DateModified = user.DateModified;

            await _userService.BeginTransactionAsync();
            await _userService.UpdateAsync(userEntity);
            await _userService.CommitAsync();
            return Ok("User updated");
        }
        catch (Exception ex)
        {
            await _userService.RollBackTransactionAsync();
            return BadRequest("Error processing your request");
        }
        finally
        {
            await _userService.DisposeAsync();
        }
    }

    [HttpDelete]
    [Route("/:id")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var userEntity = await _userService.GetAsync(id);
            
            if (userEntity is null)
            {
                return NotFound("User not found");
            }

            await _userService.BeginTransactionAsync();
            await _userService.RemoveAsync(id);
            await _userService.CommitAsync();

            return Ok("User deleted");
        }
        catch (Exception ex)
        {
            await _userService.RollBackTransactionAsync();
            return BadRequest("Error processing your request");
        }
        finally
        {
            await _userService.DisposeAsync();
        }
    }
    #endregion
}
