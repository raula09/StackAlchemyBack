using StackAlchemy.Models;
using Microsoft.EntityFrameworkCore;
using StackAlchemy.Models;
using StackAlchemy.Repositories;

namespace StackAlchemy.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StackContext _context;

        public UserRepository(StackContext context)
        {
            _context = context;
        }

        public User CreateUser(User user)
        {
            var newUser = new User
            {
                Username = user.Username,
                Email = user.Email,
                PasswordHash = PasswordHelper.HashPassword(user.PasswordHash)
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }

        public User DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return null;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return user;
        }

        public User GetUserById(int id)
        {
            return _context.Users
                .Where(u => u.Id == id)
                .Select(u => new User
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email
                })
                .FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return _context.Users
                .Select(u => new User
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email
                })
                .ToList();
        }

        public User UpdateUser(int id, User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null) return null;

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;

            _context.SaveChanges();
            return existingUser;
        }

        public List<Question> SearchQuestions(string questionName)
        {
            return _context.Questions
                           .Where(q => q.Title.Contains(questionName) || q.Description.Contains(questionName))
                           .Include(q => q.Answers)
                           .Select(q => new Question
                           {
                               Id = q.Id,
                               UserId = q.UserId,
                               Title = q.Title,
                               Description = q.Description,
                               Code = q.Code,
                               Answers = q.Answers.Select(a => new Answer
                               {
                                   Id = a.Id,
                                   QuestionId = a.QuestionId,
                                   UserId = a.UserId,
                                   Description = a.Description
                               }).ToList()
                           })
                           .ToList();
        }
    }
}
