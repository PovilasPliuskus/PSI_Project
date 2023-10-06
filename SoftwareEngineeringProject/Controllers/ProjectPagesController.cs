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
            var noteString = testNote.ToStringToSend();
            _noteService.AddNote(testNote);
            return Content(noteString, "application/Json");
        }
        public IActionResult SaveNote([FromBody] List<TempNoteData> tempNotes)
        {
            List <Note> notes = _noteService.GetNotes();
            try
            {
                foreach (var noteOriginal in notes)
                {
                    foreach (var tempNote in tempNotes)
                    {
                        if (int.Parse(noteOriginal.GetId()) == tempNote.Id)
                        {
                            noteOriginal.Name = tempNote.Name;
                            noteOriginal.Value = tempNote.Value;
                            noteOriginal.Rows = tempNote.Rows;
                            noteOriginal.Columns = tempNote.Columns;
                            break;
                        }
                    }
                }

                _noteService.SaveToFile("NoteLibrary/noteData.json", notes);

                return Content("Notes saved successfully.", "text/plain");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving notes: " + ex.Message);
                return Content("Error saving notes: " + ex.Message, "text/plain");
            }
        }
    }
}
