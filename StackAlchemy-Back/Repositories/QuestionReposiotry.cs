using StackAlchemy_Back.Models;

public class QuestionRepository
{
    private readonly StackContext _context;

    public QuestionRepository(StackContext context)
    {
        _context = context;
    }

    public Question CreateQuestion(int UserId, string Title, string Code, string Description)
    {
        Question NewQuestion = new Question
        {
            User_Id = UserId,
            Title = Title,
            Code = Code,
            Description = Description,
            Scores = 0
        };

        _context.Questions.Add(NewQuestion);
        _context.SaveChanges();
        return NewQuestion;
    }

    public List<Question> GetAllQuestions()
    {
        return _context.Questions.ToList();
    }
}