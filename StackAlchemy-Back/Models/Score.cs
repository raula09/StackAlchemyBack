using StackAlchemy_Back.Models;

public class Score
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }


    public int? AnswerId { get; set; }
    public Answer Answer { get; set; }


    public int? QuestionId { get; set; }
    public Question Question { get; set; }

    public bool IsLike { get; set; }

}