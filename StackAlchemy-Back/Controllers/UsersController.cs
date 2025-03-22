using Microsoft.AspNetCore.Mvc;
using StackAlchemy_Back.Models;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserRepository _UserRepository;
    private readonly TokenService _tokenService;
    private readonly PasswordService _passwordService;

    public UserController(UserRepository UserRepository, TokenService tokenService, PasswordService passwordService)
    {
        _UserRepository = UserRepository;
        _tokenService = tokenService;
        _passwordService = passwordService;

    }

    [HttpPost("RegisterUser")]
    public IActionResult RegisterUser(UserRegistrationDto userDetails)
    {
        string hashedPassword = _passwordService.HashPassword(userDetails.Password);

        User CreatedUser = _UserRepository.CreateUser(userDetails.Username, userDetails.Email, hashedPassword);
        if (CreatedUser == null)
        {
            return BadRequest(new { mesage = "User Creation(Registration) Failed." });
        }
        string StringToken = _tokenService.GenerateToken(CreatedUser);
        if (StringToken == null)
        {
            return BadRequest(new { message = "error on generating token." });
        }
        return Ok(new { token = StringToken });
    }


    [HttpPost("LoginUser")]

    public IActionResult LoginUser(string Username, string Password)
    {
        User LoggedInUser = _UserRepository.GetUser(Username);
        if (LoggedInUser == null)
        {
            return BadRequest(new { message = "User was not found." });
        }

        bool correctPassword = _passwordService.VerifyPassword(LoggedInUser.Password, Password);
        if (correctPassword == false)
        {
            return BadRequest(new { mesage = "invalid password" });
        }
        string StringToken = _tokenService.GenerateToken(LoggedInUser);
        if (StringToken == null)
        {
            return BadRequest(new { message = "error on generating token." });
        }
        return Ok(new { token = StringToken });
    }

    [HttpGet("GetAllUsers")]
    public IActionResult GetAllUsers()
    {
        return Ok(_UserRepository.GetAllUsers());
    }
}