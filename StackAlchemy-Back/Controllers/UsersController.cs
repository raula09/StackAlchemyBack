using Microsoft.AspNetCore.Mvc;
using StackAlchemy_Back.Models;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserRepository _UserRepository;
    private readonly TokenService _tokenService;
    private readonly PasswordService _passwordService;
    private readonly EmailService _emailService;
    private readonly StackContext _context;

    public UserController(UserRepository UserRepository, TokenService tokenService, PasswordService passwordService, EmailService emailService, StackContext context)
    {
        _UserRepository = UserRepository;
        _tokenService = tokenService;
        _passwordService = passwordService;
        _emailService = emailService;
        _context = context;

    }

    [HttpPost("RegisterUser")]
    public IActionResult RegisterUser(UserRegistrationDto userDetails)
    {
        string hashedPassword = _passwordService.HashPassword(userDetails.Password);
        string token = Guid.NewGuid().ToString();

        var newUser = _UserRepository.CreateUser(
            userDetails.Username,
            userDetails.Email,
            hashedPassword,
            token
        );

        if (newUser == null)
        {
            return BadRequest(new { message = "User already exists." });
        }

        string verifyUrl = $"http://localhost:5135/api/User/VerifyEmail?token={token}";
        string emailBody = $@"
        <p>Hello {newUser.Username},</p>
        <p>Please verify your email by clicking the button below:</p>
        <a href='{verifyUrl}' style='padding:10px 15px;background-color:#007bff;color:white;border-radius:5px;text-decoration:none;'>Verify Email</a>
        <p>This link expires in 1 hour.</p>";

        _emailService.SendEmail(newUser.Email, "Email Verification", emailBody);

        return Ok(new { message = "Verification email sent. Please verify to complete registration." });
    }

    [HttpPost("LoginUser")]
    public IActionResult LoginUser(UserLoginDto userDetails)
    {
        try
        {
            User LoggedInUser = _UserRepository.GetUser(userDetails.Email);

            if (LoggedInUser == null)
            {
                return BadRequest(new { message = "User was not found." });
            }

            bool correctPassword = _passwordService.VerifyPassword(LoggedInUser.Password, userDetails.Password);
            if (!correctPassword)
            {
                return BadRequest(new { message = "Invalid password." });
            }

            string StringToken = _tokenService.GenerateToken(LoggedInUser);
            if (StringToken == null)
            {
                return BadRequest(new { message = "Error generating token." });
            }

            return Ok(new { token = StringToken, message = $"User {LoggedInUser.Username} has logged in." });
        }
        catch (InvalidOperationException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }


    [HttpGet("VerifyEmail")]
    public IActionResult VerifyEmail(string token)
    {
        var user = _context.Users.FirstOrDefault(u => u.EmailVerificationToken == token);

        if (user == null)
        {
            return Content("Invalid or expired token.");
        }
        user.IsVerified = true;
        user.EmailVerificationToken = null;
        _context.SaveChanges();

        return Content("✅ Email verified successfully. You can now close this page.");
    }


    [HttpGet("GetAllUsers")]
    public IActionResult GetAllUsers()
    {
        return Ok(_UserRepository.GetAllUsers());
    }
}