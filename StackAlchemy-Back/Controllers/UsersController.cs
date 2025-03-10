using Microsoft.AspNetCore.Mvc;
using StackAlchemy_Back.Models;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserRepository _UserRepository;

    public UserController(UserRepository UserRepository)
    {
        _UserRepository = UserRepository;
    }

    [HttpPost("CreateUser")]
    public IActionResult CreateUser(string Username, string Email, string Password)
    {
        User CreatedUser = _UserRepository.CreateUser(Username, Email, Password);
        if (CreatedUser == null)
        {
            return BadRequest();
        }
        return Ok(CreatedUser);
    }

    [HttpGet("GetAllUsers")]
    public IActionResult GetAllUsers()
    {
        return Ok(_UserRepository.GetAllUsers());
    }
}