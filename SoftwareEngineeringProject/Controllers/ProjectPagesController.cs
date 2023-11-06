using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Enums;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.Services;

namespace SoftwareEngineeringProject.Controllers
{
    // Notes Controller
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
                _noteService.SaveToFile("Data/noteData.json", _noteService.GetNotes());
                _noteService.PrintNotesWordCount();

                return Ok("Notes saved to server successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error saving notes to server: " + ex.Message);
            }
        }
        public IActionResult LoadNotes()
        {
                _noteService.LoadFromFile("Data/noteData.json");
                return Json(_noteService.GetNotes());
        }

        [HttpPost]
        public IActionResult SortNotes(string sortOption)
        {
            NoteComparer comparer = null;
            List<Note> notes = new List<Note>();
            notes = _noteService.GetNotes();

            switch (sortOption)
            {
                case "nameAsc":
                    comparer = new NoteComparer(ComparisonType.Name);
                    break;
                case "nameDesc":
                    comparer = new NoteComparer(ComparisonType.Name);
                    break;
                case "creationDateAsc":
                    comparer = new NoteComparer(ComparisonType.CreationDate);
                    break;
                case "creationDateDesc":
                    comparer = new NoteComparer(ComparisonType.CreationDate);
                    break;
                default:
                    break;
            }

            if (comparer != null)
            {
                if (sortOption.EndsWith("Asc"))
                {
                    notes = notes.OrderBy(note => note, comparer).ToList();
                    _noteService.ReplaceNotes(notes);
                }
                else if (sortOption.EndsWith("Desc"))
                {
                    notes = notes.OrderByDescending(note => note, comparer).ToList();
                    _noteService.ReplaceNotes(notes);
                }
            }

            return Json(_noteService.GetNotes());
        }
    }
}
