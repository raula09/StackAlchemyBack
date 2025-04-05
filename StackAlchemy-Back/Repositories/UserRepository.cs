using StackAlchemy_Back.Models;

public class UserRepository
{
    private readonly StackContext _context;

    public UserRepository(StackContext context)
    {
        _context = context;
    }

    public User CreateUser(string Username, string Email, string Password, string token)
    {
        if (_context.Users.Any(u => u.Username == Username || u.Email == Email))
            return null;

        var user = new User
        {
            Username = Username,
            Email = Email,
            Password = Password,
            EmailVerificationToken = token,
            IsVerified = false
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }


    public User GetUser(string Email)
    {
        User LoggedInUser = _context.Users.FirstOrDefault(u => u.Email == Email);
        if (LoggedInUser == null)
        {
            return null;
        }

        if (LoggedInUser.IsVerified == false)
        {
            throw new InvalidOperationException("User is not Verified");
        }

        return LoggedInUser;
    }

    public List<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }
}