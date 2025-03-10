using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackAlchemy_Back.Models;
using StackAlchemy_Back.Models;
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
        public IActionResult CreateUser([FromBody] User user1)
        {
            if (user1 == null)
            {
                return BadRequest("User data is null.");
            }

         
            var existingUserByEmail = _context.Users
                .FirstOrDefault(u => u.Email == user1.Email);
            if (existingUserByEmail != null)
            {
                return Conflict("Email already in use.");
            }

     
            var existingUserByUsername = _context.Users
                .FirstOrDefault(u => u.Username == user1.Username);
            if (existingUserByUsername != null)
            {
                return Conflict("Username already in use.");
            }

            
            var user = _userRepository.CreateUser(user1);

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
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }

            var updatedUser = _userRepository.UpdateUser(id, user);

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
        [HttpGet("search/{questionName}")]
        public IActionResult GetQuestion(string questionName)
        {
            var questions = _userRepository.SearchQuestions(questionName);

            if (questions == null || questions.Count == 0)
                return NotFound("No matching questions found.");

            return Ok(questions);
        }
    }
}
