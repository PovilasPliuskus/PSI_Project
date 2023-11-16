using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareEngineeringProject.Enums;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.Services;

namespace SoftwareEngineeringProject.Controllers
{
    // Notes Controller
    public class NotesController : Controller
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
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

        // 
        [HttpPost]
        public IActionResult SaveNotes([FromBody] List<Note> clientNotes)
        {
            try
            {
                // Get the list of existing notes from the database
                List<Note> existingNotes = _noteService.GetNotesFromDatabase();

                foreach (var note in clientNotes)
                {
                    var existingNote = existingNotes.FirstOrDefault(n => n.Id == note.Id);

                    if (existingNote != null)
                    {
                        // Note with the same ID exists, update it with the new values
                        existingNote.Name = note.Name;
                        existingNote.Value = note.Value;
                        _noteService.UpdateNote(existingNote);
                    }
                    else
                    {
                        // Note with the same ID doesn't exist, add the new note
                        _noteService.AddNote(note);
                    }
                }

                // After processing all clientNotes, you may want to clean up the database by removing notes that are no longer in the clientNotes list. This depends on your requirements.

                _noteService.PrintList(); // Print the list (optional)

                return Ok("Notes saved to the database successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error saving notes to the database: " + ex.Message);
            }
        }

        public IActionResult LoadNotes()
        {
            try
            {
                List<Note> notes = _noteService.GetNotesFromDatabase();
                return Json(notes);
            }
            catch (Exception ex)
            {
                return BadRequest("Error loading notes from the database: " + ex.Message);
            }
        }

        /*        public IActionResult LoadNotes()
                {
                    _noteService.LoadFromFile("Data/noteData.json");
                    return Json(_noteService.GetNotes());
                }*/

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
