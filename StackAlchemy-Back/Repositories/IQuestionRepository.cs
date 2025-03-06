using StackAlchemy_Back.Models;
using StackAlchemy_Back.Models.DTO;

namespace StackAlchemy_Back.Repositories
{
    public interface IQuestionRepository
    {
       
        Question AskQuestion(int userId, QuestionDto questionDto);

       
        Answer AnswerQuestion(int questionId, int userId, AnswerDto answerDto);

       
        List<QuestionDto> GetQuestionsWithAnswers();
    }
}
