using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareEngineeringProject.Enums;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.Services;

namespace SoftwareEngineeringProject.Controllers
{
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

        public async Task<IActionResult> CreateNote()
        {
            try
            {
                // Get the connected user ID from the session
                var connectedUserId = HttpContext.Session.GetString("ConnectedUserId");

                var testNote = new Note { Value = "Hello" };
                await _noteService.AddNoteAsync(testNote, connectedUserId);

                return Json(testNote);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating note: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveNotes([FromBody] List<Note> clientNotes)
        {
            try
            {
                // Get the connected user ID from the session
                var connectedUserId = HttpContext.Session.GetString("ConnectedUserId");

                // Filter existing notes by user ID
                List<Note> existingNotes = _noteService.GetNotesFromDatabase(connectedUserId)
                    .Where(n => n.UserId == connectedUserId)
                    .ToList();

                foreach (var note in clientNotes)
                {
                    var existingNote = existingNotes.FirstOrDefault(n => n.Id == note.Id);

                    if (existingNote != null)
                    {
                        existingNote.Name = note.Name;
                        existingNote.Value = note.Value;
                        await _noteService.UpdateNoteAsync(existingNote, connectedUserId);
                    }
                    else
                    {
                        // Set the user ID for new notes
                        note.UserId = connectedUserId;
                        await _noteService.AddNoteAsync(note, connectedUserId);
                    }
                }

                return Ok("Notes saved to the database successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error saving notes to the database: {ex.Message}");
            }
        }

        public IActionResult LoadNotes()
        {
            try
            {
                // Get the connected user ID from the session
                var connectedUserId = HttpContext.Session.GetString("ConnectedUserId");

                // Filter notes by user ID
                List<Note> notes = _noteService.GetNotesFromDatabase(connectedUserId)
                    .Where(n => n.UserId == connectedUserId)
                    .ToList();

                return Json(notes);
            }
            catch (Exception ex)
            {
                return BadRequest("Error loading notes from the database: " + ex.Message);
            }
        }


        [HttpPost]
        public IActionResult RemoveNote(Guid noteId)
        {
            try
            {
                var noteToRemove = _noteService.GetNoteById(noteId);

                if (noteToRemove != null)
                {
                    _noteService.RemoveNote(noteToRemove);
                    return Ok(new { success = true, message = $"Note with ID {noteId} removed successfully." });
                }
                else
                {
                    return NotFound(new { success = false, message = $"Note with ID {noteId} not found." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error removing note from the database: " + ex.Message });
            }
        }


/*        [HttpPost]
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
        }*/
    }
}
