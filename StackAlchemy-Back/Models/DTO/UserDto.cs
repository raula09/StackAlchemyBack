using System.ComponentModel.DataAnnotations;

namespace StackAlchemy_Back.Models.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
       
    }
}
