using StackAlchemy_Back.Models;
using StackAlchemy_Back.Models.DTO;

namespace StackAlchemy_Back.Repositories
{
    public interface IUserRepository
    {
        List<UserDto>GetUsers();
        UserDto GetUserById(int id);
        User UpdateUser(int  id, UserDto user); 
        User CreateUser(UserDto user);
        User DeleteUser(int id);
    }
}
