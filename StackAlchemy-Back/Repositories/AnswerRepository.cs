using StackAlchemy_Back.Models;

public class AnswerRepository
{
    private readonly StackContext _context;

    public AnswerRepository(StackContext context)
    {
        _context = context;
    }

    public Answer CreateAnswer(int UserId, int QuestionId, string Title, string Code, string Description)
    {
        Answer NewAnswer = new Answer
        {
            User_Id = UserId,
            Question_Id = QuestionId,
            Title = Title,
            Code = Code,
            Description = Description,
        };

        _context.Answers.Add(NewAnswer);
        _context.SaveChanges();
        return NewAnswer;
    }

    public List<Answer> GetQuestionsAnswers(int QuestionId)
    {
        return _context.Answers.Where(a => a.Question_Id == QuestionId).ToList();
    }
    public List<Answer> GetAllAnswers()
    {
        return _context.Answers.ToList();
    }

    public (int likes, int dislikes) GetLikesAndDislikes(int answerId)
    {
        var likes = _context.Scores.Count(s => s.AnswerId == answerId && s.IsLike == true);
        var dislikes = _context.Scores.Count(s => s.AnswerId == answerId && s.IsLike == false);
        return (likes, dislikes);
    }

    public bool LikeAnswer(int userId, int answerId)
    {
        var existingLike = _context.Scores.FirstOrDefault(s => s.UserId == userId && s.AnswerId == answerId && s.IsLike == true);
        if (existingLike != null)
        {
            return false;
        }

        Score score = new Score
        {
            UserId = userId,
            AnswerId = answerId,
            IsLike = true
        };

        return true;
    }

    public bool DislikeAnswer(int userId, int answerId)
    {
        var existingDislike = _context.Scores.FirstOrDefault(s => s.UserId == userId && s.AnswerId == answerId && s.IsLike == false);
        if (existingDislike != null)
        {
            return false;
        }

        Score score = new Score
        {
            UserId = userId,
            AnswerId = answerId,
            IsLike = false
        };

        return true;

    }
}