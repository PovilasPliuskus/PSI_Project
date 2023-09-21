using System.IO;
using System.Text.Json;
namespace SoftwareEngineeringProject.NoteLibrary
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

        public static List<Note> Notes {get;set;} = new List<Note>(); //property for holding multiple note class objects
        
        public void SaveToFile(string filepath)
        {
            try
            {
                using (StreamWriter fileWriter = File.CreateText(filepath))
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true // For pretty formatting in the JSON file
                    };

                    // Serialize the entire Notes collection
                    var jsonString = JsonSerializer.Serialize(Notes, options);
                    fileWriter.Write(jsonString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving notes: " + ex.Message);
            }
        }
        public static void LoadFromFile(string filePath)
        {
            try
            {
                using (StreamReader fileReader = File.OpenText(filePath))
                {
                    var jsonString = fileReader.ReadToEnd();

                    // Deserialize the entire Notes collection
                    Notes = JsonSerializer.Deserialize<List<Note>>(jsonString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading notes: " + ex.Message);
            }
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
