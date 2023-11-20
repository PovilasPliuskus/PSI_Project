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

        [HttpPost]
        public IActionResult LogIn([FromBody] UserLoginModel loginModel)
        {
            try
            {
                // Check if the email and password match
                var user = _userService.GetUserByEmailAndPassword(loginModel.Email, loginModel.Password);

                if (user != null)
                {
                    HttpContext.Session.SetString("ConnectedUserId", user.UserId.ToString());
                    HttpContext.Session.SetString("ConnectedUserFirstName", user.FirstName);

                    Console.WriteLine($"Connected User ID: {HttpContext.Session.GetString("ConnectedUserId")}");
                    Console.WriteLine($"Connected User Name: {HttpContext.Session.GetString("ConnectedUserFirstName")}");

                    return Ok("Authentication successful!");
                }
                else
                {
                    return BadRequest("Invalid email or password.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error during login: {ex.Message}");
            }
        }
    }
}
