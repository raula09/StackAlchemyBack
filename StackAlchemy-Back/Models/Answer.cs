using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StackAlchemy_Back.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public User User { get; set; }
        public int Question_Id { get; set; }
        public Question Question { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Code { get; set; }
        public int Scores { get; set; }
    }

}
