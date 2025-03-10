using StackAlchemy_Back.Models;

public class UserRepository
{
    private readonly StackContext _context;

    public UserRepository(StackContext context)
    {
        _context = context;
    }

    public User CreateUser(string Username, string Email, string Password)
    {
        User NewUser = new User
        {
            Username = Username,
            Email = Email,
            Password = Password
        };

        _context.Users.Add(NewUser);
        _context.SaveChanges();
        return NewUser;
    }

    public List<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }
}