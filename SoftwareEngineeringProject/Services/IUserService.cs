using SoftwareEngineeringProject.Models;

namespace SoftwareEngineeringProject.Services
{
    public interface IUserService
    {
        public void PrintUsers();
        public void CreateUser(User newUser);
        public User GetUserByEmailAndPassword(string email, string password);
    }
}
