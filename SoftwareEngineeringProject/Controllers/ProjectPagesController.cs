using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.NoteLibrary;

namespace SoftwareEngineeringProject.Controllers
{
    public class ProjectPagesController : Controller
    {
        public IActionResult NotePage()
        {
            return View();
        }

        public IActionResult CreateNote()
        {
            var testNote = new Note(value: "Hello", name:"New Note");
            var noteString = testNote.ToStringToSend();

            return Content(noteString, "application/json");
        }
    }
}
