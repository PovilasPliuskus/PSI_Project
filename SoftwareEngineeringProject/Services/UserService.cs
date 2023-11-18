using SoftwareEngineeringProject.Models;

namespace SoftwareEngineeringProject.Services
{
    public class UserService : IUserService
    {
        private readonly NoteDBContext _context;
        private static List<User> _users = new List<User>();

        public UserService(NoteDBContext context)
        {
            _context = context;
        }

        public void PrintUsers()
        {
            Console.WriteLine("NUMBER OF USERS: " + _users.Count());
            foreach (var user in _users)
            {
                Console.WriteLine("ID: " + user.UserId);
                Console.WriteLine("First Name: " + user.FirstName);
                Console.WriteLine("Last Name: " + user.LastName);
                Console.WriteLine("Email Address: " + user.EmailAddress);
                Console.WriteLine("Password: " + user.Password);
                Console.WriteLine();
            }
        }

        public void CreateUser(User newUser)
        {
            try
            {
                newUser.UserId = Guid.NewGuid();
                _users.Add(newUser);


                _context.Users.Add(newUser);
                _context.SaveChanges();

                Console.WriteLine("User created and saved to the database successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating user: " + ex.Message);
            }
        }
    }
}
