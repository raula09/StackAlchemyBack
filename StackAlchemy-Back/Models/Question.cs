using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StackAlchemy_Back.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Code { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Views { get; set; }
        public int Score { get; set; }
        public bool IsClosed { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }

    //test comment
}
