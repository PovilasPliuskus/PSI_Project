using System.Text.Json;
using SoftwareEngineeringProject.Extensions;
using SoftwareEngineeringProject.Models;

namespace SoftwareEngineeringProject.Services
{
    public class NoteService : INoteService
    {
        private readonly NoteDBContext _context;

        public NoteService(NoteDBContext context)
        {
            _context = context;
        }

        public void SaveToFile<T>(string filepath, List<T> items)
        {
            try
            {
                using (StreamWriter fileWriter = File.CreateText(filepath))
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };

                    var jsonString = JsonSerializer.Serialize(items, options);
                    fileWriter.Write(jsonString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving notes: " + ex.Message);
            }
        }

        public void AddNote(Note note, string connectedUserId)
        {
            // Assign the connected user ID to the note
            note.UserId = connectedUserId;

            // Save the note to the database
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public bool NoteExists(Guid noteId)
        {
            return _context.Notes.Any(n => n.Id == noteId);
        }

        public void UpdateNote(Note updatedNote, string connectedUserId)
        {

            // Ensure that the user owns the note before updating
            if (_context.Notes.Any(n => n.Id == updatedNote.Id && n.UserId == connectedUserId))
            {
                var existingNote = _context.Notes.FirstOrDefault(n => n.Id == updatedNote.Id);

                if (existingNote != null)
                {
                    // Update the existing note with the new values
                    existingNote.Name = updatedNote.Name;
                    existingNote.Value = updatedNote.Value;

                    // Save changes to the database
                    _context.SaveChanges();
                }
            }
            else
            {
                // Handle unauthorized update (the user does not own the note)
                throw new InvalidOperationException("Unauthorized update attempt.");
            }
        }

        public List<Note> GetNotesFromDatabase(string connectedUserId)
        {

            // Filter notes by user ID
            return _context.Notes.Where(n => n.UserId == connectedUserId).ToList();
        }

        public Note GetNoteById(Guid noteId)
        {
            return _context.Notes.FirstOrDefault(n => n.Id == noteId);
        }

        public void RemoveNote(Note noteToRemove)
        {
            try
            {
                var existingNote = _context.Notes.FirstOrDefault(n => n.Id == noteToRemove.Id);

                if (existingNote != null)
                {
                    _context.Notes.Remove(existingNote);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error removing note: " + ex.Message);
                throw;
            }
        }
    }
}
