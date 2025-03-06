using Microsoft.AspNetCore.Mvc;
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

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Create a new user
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null.");
            }

            var user = _userRepository.CreateUser(userDto);

            if (user == null)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // Get user by ID
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

        // Get all users
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();

            return Ok(users);
        }

        // Update an existing user
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

        // Delete a user
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
