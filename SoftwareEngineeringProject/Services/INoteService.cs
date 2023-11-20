using SoftwareEngineeringProject.Models;
using System.Collections.Generic;

namespace SoftwareEngineeringProject.Services
{
    public interface INoteService
    {
        public void SaveToFile<T>(string filepath, List<T> items);
        Task AddNoteAsync(Note note, string connectedUserId);
        public bool NoteExists(Guid noteId);
        Task UpdateNoteAsync(Note updatedNote, string connectedUserId);
        public List<Note> GetNotesFromDatabase(string connectedUserId);
        public Note GetNoteById(Guid noteId);
        public void RemoveNote(Note noteToRemove);
    }
}
