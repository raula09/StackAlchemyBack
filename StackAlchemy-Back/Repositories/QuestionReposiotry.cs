using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using StackAlchemy_Back.Models;

public class QuestionRepository
{
    private readonly StackContext _context;
    private readonly TokenService _tokenService;

    public QuestionRepository(StackContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public QuestionDto GetQuestionById(int questionId)
    {
        Question question = _context.Questions
        .Include(q => q.User)
        .Include(q => q.Answers)
            .ThenInclude(a => a.User)
        .FirstOrDefault(q => q.Id == questionId);

        QuestionDto questionDto = new QuestionDto
        {
            Id = question.Id,
            Title = question.Title,
            Description = question.Description,
            Code = question.Code,
            User = new UserDto
            {
                Id = question.User_Id,
                Username = question.User.Username
            },
            Answers = question.Answers.Select(a => new AnswerDto
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                Code = a.Code,
                User = new UserDto
                {
                    Id = a.User_Id,
                    Username = a.User.Username
                }
            }).ToList()
        };

        return questionDto;
    }

    public QuestionDto CreateQuestion(string token, string Title, string Code, string Description)
    {

        var UserDetails = _tokenService.ValidateToken(token);
        if (UserDetails == null)
        {
            throw new UnauthorizedAccessException("Invalid token.");
        }


        var userIdClaim = UserDetails.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            throw new UnauthorizedAccessException("User ID not found in token.");
        }

        var userIdString = int.Parse(userIdClaim.Value);

        Question NewQuestion = new Question
        {
            User_Id = userIdString,
            Title = Title,
            Code = Code,
            Description = Description,
        };

        _context.Questions.Add(NewQuestion);
        _context.SaveChanges();

        var questionDTO = new QuestionDto
        {
            Id = NewQuestion.Id,
            Title = NewQuestion.Title,
            Code = NewQuestion.Code,
            Description = NewQuestion.Description,
        };
        return questionDTO;
    }

    public List<QuestionDto> GetAllQuestions()
    {
        return _context.Questions
            .Include(q => q.Answers)
            .Include(q => q.Scores)
            .Select(q => new QuestionDto
            {
                Id = q.Id,
                Title = q.Title,
                Code = q.Code,
                Description = q.Description,
                Scores = q.Scores.Count(),

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