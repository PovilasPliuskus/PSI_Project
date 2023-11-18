using System.Text.Json;
using SoftwareEngineeringProject.Extensions;
using SoftwareEngineeringProject.Models;

namespace SoftwareEngineeringProject.Services
{
    public class NoteService : INoteService
    {
        private List<Note> _notes = new List<Note>();
        private readonly NoteDBContext _context;

        public NoteService(NoteDBContext context)
        {
            _context = context;
        }

        public List<Note> GetNotes() { return _notes; }
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
        public void LoadFromFile(string filePath)
        {
            try
            {
                using (StreamReader fileReader = File.OpenText(filePath))
                {
                    var jsonString = fileReader.ReadToEnd();
                    _notes = JsonSerializer.Deserialize<List<Note>>(jsonString); //returns a list of notes from file
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading notes: " + ex.Message);
            }
        }

        public void AddNote(Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public void PrintList()
        {
            Console.WriteLine("Number of notes: " + _notes.Count);
            foreach (Note note in _notes)
            {
                note.ToString();
            }
        }

        public void ReplaceNotes(List<Note> newNotes)
        {
            _notes.Clear();

            // Add the new list of notes to the service
            _notes.AddRange(newNotes);
        }

        public void PrintNotesWordCount()
        {
            foreach(Note note in _notes)
            {
                Console.WriteLine(note.Value.WordCount());
            }
        }

        public bool NoteExists(Guid noteId)
        {
            return _context.Notes.Any(n => n.Id == noteId);
        }

        public void UpdateNote(Note updatedNote)
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

        public List<Note> GetNotesFromDatabase()
        {
            return _context.Notes.ToList();
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
