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
        User UserIsRegistered = _context.Users.FirstOrDefault(u => u.Username == Username || u.Email == Email);
        if (UserIsRegistered != null)
        {
            return null;
        }
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

    public User GetUser(string Username)
    {
        User LoggedInUser = _context.Users.FirstOrDefault(u => u.Username == Username);
        if (LoggedInUser == null)
        {
            return null;
        }

        return LoggedInUser;
    }

    public List<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }
}