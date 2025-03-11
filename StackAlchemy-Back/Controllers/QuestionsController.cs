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
    public IActionResult CreateQuestion(int UserId, string Title, string Code, string Description)
    {
        var user = _context.Users.Find(UserId);
        if (user == null)
        {
            return BadRequest();
        }
        Question CreatedQuestion = _questionRepository.CreateQuestion(UserId, Title, Code, Description);
        if (CreatedQuestion == null)
        {
            return BadRequest();
        }
        return Ok(CreatedQuestion);
    }

    [HttpGet("GetAllQuestions")]
    public IActionResult GetAllQuestions()
    {
        return Ok(_questionRepository.GetAllQuestions());
    }
}