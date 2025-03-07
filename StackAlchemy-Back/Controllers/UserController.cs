using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackAlchemy_Back.Models;
using StackAlchemy_Back.Models.DTO;
using StackAlchemy_Back.Repositories;

namespace StackAlchemy_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly StackContext _context;

        public UserController(IUserRepository userRepository, StackContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

      
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null.");
            }

         
            var existingUserByEmail = _context.Users
                .FirstOrDefault(u => u.Email == userDto.Email);
            if (existingUserByEmail != null)
            {
                return Conflict("Email already in use.");
            }

     
            var existingUserByUsername = _context.Users
                .FirstOrDefault(u => u.Username == userDto.Username);
            if (existingUserByUsername != null)
            {
                return Conflict("Username already in use.");
            }

            
            var user = _userRepository.CreateUser(userDto);

            if (user == null)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }


        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

      
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();

            return Ok(users);
        }

      
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null.");
            }

            var updatedUser = _userRepository.UpdateUser(id, userDto);

            if (updatedUser == null)
            {
                return NotFound("User not found.");
            }

            return Ok(updatedUser);
        }

       
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var deletedUser = _userRepository.DeleteUser(id);

            if (deletedUser == null)
            {
                return NotFound("User not found.");
            }

            return Ok(deletedUser);
        }

    }
}
