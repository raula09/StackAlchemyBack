using Microsoft.EntityFrameworkCore;
using StackAlchemy_Back.Models;

public class QuestionRepository
{
    private readonly StackContext _context;

    public QuestionRepository(StackContext context)
    {
        _context = context;
    }

    public QuestionDTO CreateQuestion(int UserId, string Title, string Code, string Description)
    {
        Question NewQuestion = new Question
        {
            User_Id = UserId,
            Title = Title,
            Code = Code,
            Description = Description,
        };

        _context.Questions.Add(NewQuestion);
        _context.SaveChanges();

        var questionDTO = new QuestionDTO
        {
            QuestionId = NewQuestion.Id,
            Title = NewQuestion.Title,
            Code = NewQuestion.Code,
            Description = NewQuestion.Description,
            UserId = NewQuestion.User_Id
        };
        return questionDTO;
    }

    public List<QuestionDTO> GetAllQuestions()
    {
        return _context.Questions
            .Include(q => q.Answers)
            .Include(q => q.Scores)
            .Select(q => new QuestionDTO
            {
                QuestionId = q.Id,
                Title = q.Title,
                Code = q.Code,
                Description = q.Description,
                UserId = q.User_Id,
                UserName = q.User.Username,
                Scores = q.Scores.Count(),
                Answers = q.Answers.Count()

            })
            .ToList();
    }

    public (int likes, int dislikes) GetLikesAndDislikes(int questionId)
    {
        var likes = _context.Scores.Count(s => s.QuestionId == questionId && s.IsLike == true);
        var dislikes = _context.Scores.Count(s => s.QuestionId == questionId && s.IsLike == false);
        return (likes, dislikes);
    }

    public bool LikeQuestion(int userId, int questionId)
    {
        var existingLike = _context.Scores
             .FirstOrDefault(s => s.UserId == userId && s.QuestionId == questionId && s.IsLike == true);

        if (existingLike != null)
        {
            return false;
        }

        Score score = new Score
        {
            UserId = userId,
            QuestionId = questionId,
            IsLike = true
        };


        _context.Scores.Add(score);
        _context.SaveChanges();

        return true;
    }

    public bool DislikeQuestion(int userId, int questionId)
    {
        var existingDislike = _context.Scores.FirstOrDefault(s => s.UserId == userId && s.QuestionId == questionId && s.IsLike == false);
        if (existingDislike != null)
        {
            return false;
        }

        Score score = new Score
        {
            UserId = userId,
            QuestionId = questionId,
            IsLike = false
        };

        _context.Scores.Add(score);
        _context.SaveChanges();

        return true;

    }
}