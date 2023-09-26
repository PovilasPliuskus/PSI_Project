using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.NoteLibrary;
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
            return View();
        }

        public IActionResult CreateNote()
        {
            var testNote = new Note(value: "Hello");
            var noteString = testNote.ToStringToSend();
            NoteList.Notes.Add(testNote); //add new note object to list
            return Content(noteString, "application/Json");
        }
        public class TempNoteData //class used for storing note data on runtime
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public string Value { get; set; } = "";
            public int Rows { get; set; }
            public int Columns { get; set; }
            public string Category { get; set; } = "";
        }
        [HttpPost]
        public IActionResult SaveNote([FromBody] List<TempNoteData> tempNotes)
        {
            try
            {
                foreach(var noteOriginal in NoteList.Notes)
                {
                    foreach(var tempNote in tempNotes)
                    {
                        if (int.Parse(noteOriginal.GetId()) == tempNote.Id)
                        {
                            noteOriginal.Name=tempNote.Name;
                            noteOriginal.Value=tempNote.Value;
                            noteOriginal.Rows = tempNote.Rows;
                            noteOriginal.Columns=tempNote.Columns;
                            break;
                        }
                    }
                }
                SaveData.SaveToFile("NoteLibrary/noteData.json");
                return Content("Notes saved successfully.", "text/plain");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving notes: " + ex.Message);
                return Content("Error saving notes: " + ex.Message, "text/plain");
            }
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