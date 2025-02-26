using System.Xml.Linq;
   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

namespace StackAlchemy_Back.Models
{
 
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Username { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string PasswordHash { get; set; }

        public DateTime RegistrationDate { get; set; }
        public int Reputation { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string ProfilePictureUrl { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Bio { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }


}
