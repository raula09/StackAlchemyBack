using Microsoft.AspNetCore.Mvc;
using StackAlchemy_Back.Models;

[ApiController]
[Route("api/[controller]")]

public class AnswerController : ControllerBase
{
    private readonly AnswerRepository _AnswerRepository;

    public AnswerController(AnswerRepository AnswerRepository)
    {
        _AnswerRepository = AnswerRepository;
    }

    [HttpPost("CreateAnswer")]
    public IActionResult CreateAnswer(int UserId, string Title, string Code, string Description)
    {
        Answer CreatedAnswer = _AnswerRepository.CreateAnswer(UserId, Title, Code, Description);
        if (CreatedAnswer == null)
        {
            return BadRequest();
        }
        return Ok(CreatedAnswer);
    }

    [HttpGet("GetAllAnswers")]
    public IActionResult GetAllAnswers()
    {
        return Ok(_AnswerRepository.GetAllAnswers());
    }
}