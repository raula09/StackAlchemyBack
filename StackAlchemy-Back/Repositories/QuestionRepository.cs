using StackAlchemy_Back.Models;
using StackAlchemy_Back.Models.DTO;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace StackAlchemy_Back.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly StackContext _context;
        private readonly IMapper _mapper;

        public QuestionRepository(StackContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Question AskQuestion(int userId, QuestionDto questionDto)
        {
            var question = new Question
            {
                UserId = userId,
                Title = questionDto.Title,
                Description = questionDto.Description
            };

            _context.Questions.Add(question);
            _context.SaveChanges();

            return question;
        }

        public Answer AnswerQuestion(int questionId, int userId, AnswerDto answerDto)
        {
            var user = _context.Users.Find(userId);
            if (user == null) return null;

            var question = _context.Questions.Find(questionId);
            if (question == null) return null;

            var answer = new Answer
            {
                Title = answerDto.Title,
                Description = answerDto.Description,
                Code = answerDto.Code,
                QuestionId = questionId,
                UserId = userId
            };

            _context.Answers.Add(answer);
            _context.SaveChanges();

            return answer;
        }

      
        public List<QuestionDto> GetQuestionsWithAnswers()
        {
            var questionsWithAnswers = _context.Questions
                .Include(q => q.Answers)  
                .ThenInclude(a => a.User)
                .ToList();

          
            var questionDtos = _mapper.Map<List<QuestionDto>>(questionsWithAnswers);

            return questionDtos;
        }
    }
}
