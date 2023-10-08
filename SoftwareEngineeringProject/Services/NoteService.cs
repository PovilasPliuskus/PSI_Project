﻿using SoftwareEngineeringProject.NoteLibrary;
using System.Text.Json;

namespace SoftwareEngineeringProject.Services
{
    public class NoteService
    {
        private List<Note> _notes = new List<Note>();

        public List<Note> GetNotes() { return _notes; }
        public void SaveToFile(string filepath, List<Note> notes)
        {
            try
            {
                using (StreamWriter fileWriter = File.CreateText(filepath))
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };

                    var jsonString = JsonSerializer.Serialize(notes, options);
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
            _notes.Add(note);
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


    }
}
