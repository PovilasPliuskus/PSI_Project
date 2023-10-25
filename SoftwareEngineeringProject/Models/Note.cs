using System.IO;
using System.Text.Json;
using SoftwareEngineeringProject.Enums;
namespace SoftwareEngineeringProject.Models
{
    public record class NoteInformationRecord(DateTime CreationDate, int Id);
    public class Note
    {
        private static int _idNumber = 0;
        private String _name;
        private String _value;
        private NoteInformationRecord _information;
        private int _rows;
        private int _columns;
        private NoteCategory _category;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public String Value
        {
            get { return _value; }
            set { _value = value.ToString(); }
        }

        public int Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        public int Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        public NoteCategory Category
        {
            get { return _category; }
            set { _columns = (int)value; }
        }
        public NoteInformationRecord InformationRecord
        {
            get { return _information; }
            set { _information = value; }
        }
        public string GetId()
        {
            return _information.Id.ToString();
        }

        public string GetCreationDate()
        {
            return _information.CreationDate.ToString();
        }

        public Note(string name = "New Note", string value = "", int rows = 10, int columns = 100, NoteCategory category = NoteCategory.Personal)
        {
            _information = CreateNoteInformationData();
            _name = name;
            _value = value;
            _rows = rows;
            _columns = columns;
            _category = category;
        }
        public NoteInformationRecord CreateNoteInformationData()
        {
            DateTime CreationDate = DateTime.Now;
            int Id = _idNumber++;

            return new NoteInformationRecord(CreationDate, Id);
        }

        public void ToString()
        {
            Console.WriteLine("id: " + GetId());
            Console.WriteLine("name: " + Name);
            Console.WriteLine("value: " + Value);
            Console.WriteLine("rows: " + Rows);
            Console.WriteLine("columns: " + Columns);
            Console.WriteLine("creation date: " + GetCreationDate());
            Console.WriteLine("category: " + Category);
            Console.WriteLine();
        }

        public string ToStringToSend()
        {
            return "{" +
                   $"\"id\": {GetId()},\n" +
                   $"\"name\": \"{Name}\",\n" +
                   $"\"value\": \"{Value}\",\n" +
                   $"\"rows\": {Rows},\n" +
                   $"\"columns\": {Columns},\n" +
                   $"\"creationDate\": \"{GetCreationDate()}\",\n" +
                   $"\"category\": \"{Category}\"\n" +
                   "}";
        }
    }
}
