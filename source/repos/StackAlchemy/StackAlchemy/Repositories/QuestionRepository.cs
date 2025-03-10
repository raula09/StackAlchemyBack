using StackAlchemy.Models;
using Microsoft.EntityFrameworkCore;

namespace StackAlchemy.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly StackContext _context;

        public QuestionRepository(StackContext context)
        {
            _context = context;
        }

        public Question AskQuestion(int userId, Question question)
        {
            var newQuestion = new Question
            {
                UserId = userId,
                Title = question.Title,
                Description = question.Description,
                Code = question.Code,
                Answers = new List<Answer>()
            };

            _context.Questions.Add(newQuestion);
            _context.SaveChanges();

            return newQuestion;
        }

        public Answer AnswerQuestion(int questionId, int userId, Answer answer)
        {
            var user = _context.Users.Find(userId);
            if (user == null) return null;

            var question = _context.Questions.Find(questionId);
            if (question == null) return null;

            var newAnswer = new Answer
            {
                Title = answer.Title,
                Description = answer.Description,
                Code = answer.Code,
                QuestionId = questionId,
                UserId = userId
            };

            _context.Answers.Add(newAnswer);
            _context.SaveChanges();

            return newAnswer;
        }

        public List<Question> GetQuestionsWithAnswers()
        {
            return _context.Questions
                .Include(q => q.Answers)
                .ThenInclude(a => a.User)
                .ToList();
        }
    }
}
