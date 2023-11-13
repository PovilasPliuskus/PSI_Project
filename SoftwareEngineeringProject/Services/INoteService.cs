using SoftwareEngineeringProject.Models;
using System.Collections.Generic;

namespace SoftwareEngineeringProject.Services
{
    public interface INoteService
    {
        List<Note> GetNotes();
        public void SaveToFile<T>(string filepath, List<T> items);
        void LoadFromFile(string filePath);
        void AddNote(Note note);
        void PrintList();
        void ReplaceNotes(List<Note> newNotes);
        public bool NoteExists(Guid noteId);
        public void UpdateNote(Note updatedNote);
        public List<Note> GetNotesFromDatabase();
    }
}
