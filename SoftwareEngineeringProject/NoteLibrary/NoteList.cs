using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.NoteLibrary
{
    public class NoteList: Note
    {
        public static List<Note> Notes {get;set;} = new List<Note>(); //property for holding multiple note class objects
    }
}