using SoftwareEngineeringProject.Models;

namespace SoftwareEngineeringProject.Services
{
    public class UserService : IUserService
    {
        private static List<User> _users = new List<User>();

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

                // You might want to save the users to a database here

                Console.WriteLine("User created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating user: " + ex.Message);
            }
        }
    }
}
