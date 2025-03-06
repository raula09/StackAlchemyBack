using StackAlchemy_Back.Models;
using StackAlchemy_Back.Models.DTO;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
//using StackAlchemy_Back.Data;
namespace StackAlchemy_Back.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StackContext _context;   
        private readonly IMapper _mapper;
        public UserRepository(StackContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public User CreateUser(UserDto user)
        {
           var U = _mapper.Map<User>(user);
            _context.Users.Add(U);
            _context.SaveChanges();
            return U;
        }

        public User DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return user;
        }

        public UserDto GetUserById(int id)
        {
            var user = _context.Users
                .Where(u => u.Id == id) 
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email
                })
                .FirstOrDefault();

            return user;
        }


        public List<UserDto> GetUsers()
        {
            var users = _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email
                  
                })
                .ToList();

            return users;
        }

        public User UpdateUser(int id, UserDto user)
        {
            var EUser = _context.Users.FirstOrDefault(u => u.Id == id);

            if (EUser == null)
            {
               
                return null;  
            }

            EUser.Username = user.Username;
            EUser.Email = user.Email;
           
            _context.SaveChanges();

            return EUser;
        }

    }
}
