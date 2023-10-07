using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.NoteLibrary;
using SoftwareEngineeringProject.Services;

namespace SoftwareEngineeringProject.Controllers
{
    public class ProjectPagesController : Controller
    {
        private NoteService _noteService;

        public ProjectPagesController(NoteService noteService)
        {
            _noteService = noteService;
        }

        public IActionResult NotePage()
        {
            return View();
        }

        public IActionResult CreateNote()
        {
            var testNote = new Note(value: "Hello");
            _noteService.AddNote(testNote);
            return Json(testNote);
        }

        [HttpPost]
        public IActionResult SaveNotes([FromBody] List<Note> clientNotes)
        {
            try
            {
                // Replace the server-side list of notes with the client-side list
                _noteService.ReplaceNotes(clientNotes);
                _noteService.SaveToFile("NoteLibrary/noteData.json", _noteService.GetNotes());

                return Ok("Notes saved to server successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error saving notes to server: " + ex.Message);
            }
        }
    }
}
