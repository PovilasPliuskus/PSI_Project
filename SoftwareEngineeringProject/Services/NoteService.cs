using System.Text.Json;
using SoftwareEngineeringProject.Extensions;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.Delegates;
using SoftwareEngineeringProject.CustomExceptions;

namespace SoftwareEngineeringProject.Services
{
    public class NoteService : INoteService
    {
        private readonly NoteDBContext _context;

        public NoteService(NoteDBContext context)
        {
            _context = context;
        }

        LogDelegate PrintConsole = (string message) =>
        {
            Console.WriteLine($"[LOG] {DateTime.Now}:  {message}");
        };

        LogDelegate PrintToFile = (string message) =>
        {
            File.AppendAllText("Data/logs.txt", $"[LOG] {DateTime.Now}:  {message}\n");
        };

        LogDelegate PrintError = (string message) =>
        {
            File.AppendAllText("Data/logs.txt", $"[ERR] {DateTime.Now}:  {message}\n");
        };

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

        public async Task AddNoteAsync(Note note, string connectedUserId)
        {
            try
            {
                // Assign the connected user ID to the note
                note.UserId = connectedUserId;

                // Save the note to the database asynchronously
                _context.Notes.Add(note);
                await _context.SaveChangesAsync();

                PrintConsole($"Note added: {note.Name}");
                PrintToFile($"Note added: {note.Name}");
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (e.g., log the exception)
                PrintError($"Error adding note: {ex.Message}");
                throw;
            }
        }


        public bool NoteExists(Guid noteId)
        {
            return _context.Notes.Any(n => n.Id == noteId);
        }

        public async Task UpdateNoteAsync(Note updatedNote, string connectedUserId)
        {
            try
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

                        // Save changes to the database asynchronously
                        await _context.SaveChangesAsync();

                        PrintConsole($"Note updated: {updatedNote.Name}");
                        PrintToFile($"Note updated: {updatedNote.Name}");
                    }
                }
                else
                {
                    // Handle unauthorized update (the user does not own the note)
                    throw new InvalidOperationException("Unauthorized update attempt.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (e.g., log the exception)
                PrintError($"Error updating note: {ex.Message}");
                throw;
            }
        }

        public List<Note> GetNotesFromDatabase(string connectedUserId)
        {
            try
            {
                // Filter notes by user ID
                return _context.Notes.Where(n => n.UserId == connectedUserId).ToList();
            }
            catch (Exception ex)
            {
                PrintError($"Exception while loading notes: {ex.Message}");

                throw new LoadFromDBException("Error loading notes from the database", PrintError);
            }

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

                    PrintConsole($"Note removed: {noteToRemove.Name}");
                    PrintToFile($"Note removed: {noteToRemove.Name}");
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
