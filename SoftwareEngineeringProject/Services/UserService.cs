﻿using SoftwareEngineeringProject.Models;

namespace SoftwareEngineeringProject.Services
{
    public class UserService : IUserService
    {
        private readonly NoteDBContext _context;
        private static List<User> _users = new List<User>();
        private readonly ConnectedUser connectedUser = new ConnectedUser();

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

        public User GetUserByEmailAndPassword(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.EmailAddress == email && u.Password == password);
        }

/*        public void LinkConnectedUser(User user)
        {
            HttpContext.Session.SetString("ConnectedUserId", user.UserId.ToString());
            HttpContext.Session.SetString("ConnectedUserFirstName", user.FirstName);
        }

        public string GetConnectedUserID()
        {
            return HttpContext.Session.GetString("ConnectedUserId");
        }*/
    }
}
