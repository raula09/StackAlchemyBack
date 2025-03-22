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
    public IActionResult CreateAnswer(int UserId, int QuestionId, string Title, string Code, string Description)
    {
        Answer CreatedAnswer = _AnswerRepository.CreateAnswer(UserId, QuestionId, Title, Code, Description);
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

    [HttpGet("GetLikesAndDislikes")]
    public IActionResult GetLikesAndDislikes([FromBody] int answerId)
    {
        return Ok(_AnswerRepository.GetLikesAndDislikes(answerId));
    }



    [HttpPost("LikeAnswer")]
    public IActionResult Likeanswer([FromBody] int userId, int answerId)
    {
        bool liked = _AnswerRepository.LikeAnswer(userId, answerId);
        if (liked == false)
        {
            return BadRequest("Erron on liking the answer.");
        }

        return Ok("Liked an answer.");
    }

    [HttpPost("DislikeAnswer")]
    public IActionResult Dislikeanswer([FromBody] int userId, int answerId)
    {
        bool disliked = _AnswerRepository.DislikeAnswer(userId, answerId);
        if (disliked == false)
        {
            return BadRequest("Error on disliking the answer.");
        }

        return Ok("Disliked an answer.");
    }
}