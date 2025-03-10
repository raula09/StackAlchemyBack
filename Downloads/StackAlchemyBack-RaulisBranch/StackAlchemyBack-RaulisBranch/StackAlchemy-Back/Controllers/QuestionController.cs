using Microsoft.AspNetCore.Mvc;
using StackAlchemy_Back.Models;
using StackAlchemy_Back.Models;
using StackAlchemy_Back.Repositories;

namespace StackAlchemy_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpPost("ask/{userId}")]
        public IActionResult AskQuestion(int userId, [FromBody] Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var question2 = _questionRepository.AskQuestion(userId, question);

            if (question == null)
            {
                return BadRequest("Failed to ask question.");
            }

            return Ok(question);
        }


        [HttpPost("answer/{questionId}/{userId}")]
        public IActionResult AnswerQuestion(int questionId, int userId, [FromBody] Answer answer)
        {
            var answer2 = _questionRepository.AnswerQuestion(questionId, userId, answer);

            if (answer2 == null)
            {
                return BadRequest("Failed to provide an answer.");
            }

            return Ok(answer);
        }

        
        [HttpGet("questions-with-answers")]
        public IActionResult GetQuestionsWithAnswers()
        {
            var questionsWithAnswers = _questionRepository.GetQuestionsWithAnswers();

            if (questionsWithAnswers == null || questionsWithAnswers.Count == 0)
            {
                return NotFound("No questions found.");
            }

            return Ok(questionsWithAnswers);
        }
    }
}
