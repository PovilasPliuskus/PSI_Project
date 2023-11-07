using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Models;
using System.Diagnostics;

namespace SoftwareEngineeringProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View("~/Views/Home/Landing.cshtml");
        
        }
        public IActionResult Login()
        {
            return View("~/Views/Home/Login.cshtml");

        }
        public IActionResult Signup()
        {
            return View("~/Views/Home/Signup.cshtml");

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}