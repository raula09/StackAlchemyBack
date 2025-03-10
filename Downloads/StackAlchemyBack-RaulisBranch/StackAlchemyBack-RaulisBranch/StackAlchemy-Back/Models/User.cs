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
   
        
        public string? Username { get; set; }
        
       
        public string? Email { get; set; }
        
        public string? PasswordHash { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Question> Questions { get; set; }
    }


}
