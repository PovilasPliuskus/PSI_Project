using SoftwareEngineeringProject.NoteLibrary;
using System.Collections.Generic;

namespace SoftwareEngineeringProject.Services
{
    public interface INoteService
    {
        List<Note> GetNotes();
        void SaveToFile(string filepath, List<Note> notes);
        void LoadFromFile(string filePath);
        void AddNote(Note note);
        void PrintList();
        void ReplaceNotes(List<Note> newNotes);
    }
}
