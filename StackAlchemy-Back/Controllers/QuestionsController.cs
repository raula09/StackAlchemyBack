using Microsoft.AspNetCore.Mvc;
using StackAlchemy_Back.Models;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly QuestionRepository _questionRepository;
    private readonly StackContext _context;

    public QuestionController(QuestionRepository questionRepository, StackContext context)
    {
        _questionRepository = questionRepository;
        _context = context;
    }

    [HttpPost("CreateQuestion")]
    public IActionResult CreateQuestion(string Authorization, string Title, string Code, string Description)
    {
        try
        {
            var token = Authorization?.Split(" ")?.Last();
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Missing token.");
            }

            QuestionDTO CreatedQuestion = _questionRepository.CreateQuestion(token, Title, Code, Description);
            if (CreatedQuestion == null)
            {
                return BadRequest("Failed to create question.");
            }

            return Ok(CreatedQuestion);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpGet("GetAllQuestions")]
    public IActionResult GetAllQuestions()
    {
        return Ok(_questionRepository.GetAllQuestions());
    }

    [HttpGet("GetLikesAndDislikes")]

    public IActionResult GetLikesAndDislikes([FromBody] int questionId)
    {
        return Ok(_questionRepository.GetLikesAndDislikes(questionId));
    }

    [HttpPost("LikeQuestion")]
    public IActionResult LikeQuestion([FromBody] int userId, int questionId)
    {
        bool liked = _questionRepository.LikeQuestion(userId, questionId);
        if (liked == false)
        {
            return BadRequest("Erron on liking the question.");
        }

        return Ok("Liked a Question.");
    }

    [HttpPost("DislikeQuestion")]
    public IActionResult DislikeQuestion([FromBody] int userId, int questionId)
    {
        bool disliked = _questionRepository.DislikeQuestion(userId, questionId);
        if (disliked == false)
        {
            return BadRequest("Error on disliking the question.");
        }

        return Ok("Disliked a Question.");
    }
}