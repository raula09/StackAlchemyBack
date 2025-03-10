using StackAlchemy_Back.Models;

public class AnswerRepository
{
    private readonly StackContext _context;

    public AnswerRepository(StackContext context)
    {
        _context = context;
    }

    public Answer CreateAnswer(int UserId, string Title, string Code, string Description)
    {
        Answer NewAnswer = new Answer
        {
            User_Id = UserId,
            Title = Title,
            Code = Code,
            Description = Description,
        };

        _context.Answers.Add(NewAnswer);
        _context.SaveChanges();
        return NewAnswer;
    }

    public List<Answer> GetAllAnswers()
    {
        return _context.Answers.ToList();
    }
}