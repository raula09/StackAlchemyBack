using StackAlchemy_Back.Models;
using StackAlchemy_Back.Models;

namespace StackAlchemy_Back.Repositories
{
    public interface IQuestionRepository
    {
       
        Question AskQuestion(int userId, Question questionDto);

       
        Answer AnswerQuestion(int questionId, int userId, Answer answerDto);

       
        List<Question> GetQuestionsWithAnswers();
    }
}
