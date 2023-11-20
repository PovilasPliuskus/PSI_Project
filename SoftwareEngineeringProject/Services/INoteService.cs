using SoftwareEngineeringProject.Models;
using System.Collections.Generic;

namespace SoftwareEngineeringProject.Services
{
    public interface INoteService
    {
        public void SaveToFile<T>(string filepath, List<T> items);
        public void AddNote(Note note, string connectedUserID);
        public bool NoteExists(Guid noteId);
        public void UpdateNote(Note updatedNote, string connectedUserId);
        public List<Note> GetNotesFromDatabase(string connectedUserId);
        public Note GetNoteById(Guid noteId);
        public void RemoveNote(Note noteToRemove);
    }
}
