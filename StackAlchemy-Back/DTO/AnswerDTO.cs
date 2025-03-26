public class AnswerDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
    public int Scores { get; set; }
    public UserDto User { get; set; }

}