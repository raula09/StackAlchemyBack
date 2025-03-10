using StackAlchemy.Models;

using StackAlchemy.Models;

namespace StackAlchemy.Repositories
{
    public interface IQuestionRepository
    {

        Question AskQuestion(int userId, Question questionDto);


        Answer AnswerQuestion(int questionId, int userId, Answer answerDto);


        List<Question> GetQuestionsWithAnswers();
    }
}
