using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.NoteLibrary
{
    public class TempNoteData //class used for storing note data on runtime
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public string Value { get; set; } = "";
            public int Rows { get; set; }
            public int Columns { get; set; }
            public string Category { get; set; } = "";
        }
}