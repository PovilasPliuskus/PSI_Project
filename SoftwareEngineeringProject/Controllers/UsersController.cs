using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.Services;

namespace SoftwareEngineeringProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserModel createUserModel)
        {
            try
            {
                var newUser = new User
                {
                    FirstName = createUserModel.FirstName,
                    LastName = createUserModel.LastName,
                    EmailAddress = createUserModel.Email,
                    Password = createUserModel.Password
                };

                _userService.CreateUser(newUser);

                _userService.PrintUsers();

                return Ok("User created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating user: {ex.Message}");
            }
        }
    }
}
