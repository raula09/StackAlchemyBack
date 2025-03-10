using StackAlchemy_Back.Models;
using StackAlchemy_Back.Models;

namespace StackAlchemy_Back.Repositories
{
    public interface IUserRepository
    {
        List<User>GetUsers();
        User GetUserById(int id);
        User UpdateUser(int  id, User user); 
        User CreateUser(User user);
        User DeleteUser(int id);
        List<Question> SearchQuestions(string questionName);
    }
}
