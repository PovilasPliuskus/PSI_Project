using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.NoteLibrary;
using System.Diagnostics;
using System.Text.Json;

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
            return View();
        }

        public IActionResult CreateNote()
        {
            var testNote = new Note(value: "Hello");

            testNote.ToString();

            return Json(testNote);
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