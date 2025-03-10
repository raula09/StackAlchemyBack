using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StackAlchemy_Back.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
       
        public string? Title { get; set; }
     
        public string? Description { get; set; }
      
        public string? Code { get; set; }
        public int? Score { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();

    }
}
