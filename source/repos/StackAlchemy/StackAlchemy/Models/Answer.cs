using StackAlchemy.Models;
using System.Text.Json.Serialization;

public class Answer
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Code { get; set; }


    [JsonIgnore]
    public User User { get; set; }
    public int UserId { get; set; }


    public int QuestionId { get; set; }
    [JsonIgnore]
    public Question Question { get; set; }
}
