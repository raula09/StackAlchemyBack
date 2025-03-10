using Microsoft.AspNetCore.Mvc;
using StackAlchemy_Back.Models;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly QuestionRepository _questionRepository;

    public QuestionController(QuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    [HttpPost("CreateQuestion")]
    public IActionResult CreateQuestion(int UserId, string Title, string Code, string Description)
    {
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