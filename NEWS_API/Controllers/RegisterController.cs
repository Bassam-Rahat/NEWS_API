namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using News_API.Models.Users;
using News_API.Authorization;
using News_API.Entities;
using News_API.Interfaces;
using News_API.Models;
using System.Data;
using News_API.Pagination;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IRegisterRepository _userService;

    public UsersController(IRegisterRepository userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<ActionResult<string>> Add(User user)
    {
        var result = _userService.Add(user);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public IActionResult Authenticate(UserDataRequest model)
    {
        var response = _userService.Authenticate(model);
        return Ok(response);
    }

    [Authorize(Role.Admin, Role.User)]
    [HttpGet("GetPaginated")]
    public ActionResult<PaginationDTO<User>> GetAll(int page)
    {
        var users = _userService.GetAll(page);
        return Ok(users);
    }

    [Authorize(Role.Admin, Role.User)]
    [HttpGet("GetFilteringandSorting")]
    public ActionResult<PaginationDTO<User>> GetFilteringandSorting(int page, string columnName, string find, string sortOrder)
    {
        var users = _userService.GetFilteringandSorting(page, columnName, find, sortOrder);
        return Ok(users);
    }

    [Authorize(Role.Admin, Role.User)]
    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<List<User>>> Get()
    {
        var result = _userService.Get();

        if (result is null)
        {
            return NotFound("No User Found");
        }
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        // only admins can access other user records
        var currentUser = (User)HttpContext.Items["User"];
        if (id != currentUser.Id && currentUser.Role != Role.Admin)
            return Unauthorized(new { message = "Unauthorized" });

        var user = _userService.GetById(id);
        return Ok(user);
    }

    [AllowAnonymous]
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(int id)
    {
        var result = _userService.Delete(id);

        if (result is null)
        {
            return NotFound("No User Found");
        }
        return Ok(result);
    }
}