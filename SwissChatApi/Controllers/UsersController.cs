using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SwissChatApi.Authorization;
using SwissChatApi.Entities;
using SwissChatApi.Helpers;
using SwissChatApi.Model.Users;
using SwissChatApi.Services;


namespace SwissChatApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public UsersController(
        IUserService userService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _userService = userService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        try
        {
            // create user
            var response = await _userService.Authenticate(model);

            return Ok(response);
        }
        catch (AppException ex)
        {
            // return error message if there was an exception
            return BadRequest(new { message = ex.Message });
        }
    }
    //Add logout
    //[AllowAnonymous]
    //[HttpPost("authenticate")]
    //public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    //{
    //    try
    //    {
    //        // create user
    //        var response = await _userService.Authenticate(model);

    //        return Ok(response);
    //    }
    //    catch (AppException ex)
    //    {
    //        // return error message if there was an exception
    //        return BadRequest(new { message = ex.Message });
    //    }
    //}
    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest model)
    {
        try
        {
            // create user
            _userService.Register(model);
            return Ok(new { message = "Registration successful" });
        }
        catch (AppException ex)
        {
            // return error message if there was an exception
            return BadRequest(new { message = ex.Message });
        }
        
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, UpdateRequest model)
    {
        try
        {
            _userService.Update(id, model);
            return Ok(new { message = "User updated successfully" });
        }
        catch (AppException ex)
        {
            // return error message if there was an exception
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _userService.Delete(id);
        return Ok(new { message = "User deleted successfully" });
    }
}

