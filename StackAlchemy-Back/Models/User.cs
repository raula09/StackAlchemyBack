using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StackAlchemy_Back.Models
{

    public class User
    {
        public int Id { get; set; }
        [Required]
        
        public string Username { get; set; }
        [Required]
       
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Question> Questions { get; set; }
    }


}
