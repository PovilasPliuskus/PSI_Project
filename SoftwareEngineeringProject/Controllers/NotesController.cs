﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareEngineeringProject.Enums;
using SoftwareEngineeringProject.Models;
using SoftwareEngineeringProject.Services;

namespace SoftwareEngineeringProject.Controllers
{
    // Notes Controller
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

        public IActionResult CreateNote()
        {
            var testNote = new Note(value: "Hello");
            _noteService.AddNote(testNote);
            return Json(testNote);
        }

        [HttpPost]
        public IActionResult SaveNotes([FromBody] List<Note> clientNotes)
        {
            try
            {
                List<Note> existingNotes = _noteService.GetNotesFromDatabase();

                foreach (var note in clientNotes)
                {
                    var existingNote = existingNotes.FirstOrDefault(n => n.Id == note.Id);

                    if (existingNote != null)
                    {
                        existingNote.Name = note.Name;
                        existingNote.Value = note.Value;
                        _noteService.UpdateNote(existingNote);
                    }
                    else
                    {
                        _noteService.AddNote(note);
                    }
                }

                _noteService.PrintList();

                return Ok("Notes saved to the database successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error saving notes to the database: " + ex.Message);
            }
        }

        public IActionResult LoadNotes()
        {
            try
            {
                List<Note> notes = _noteService.GetNotesFromDatabase();
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


        [HttpPost]
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
        }
    }
}
