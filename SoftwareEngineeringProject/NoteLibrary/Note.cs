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

        public int GetId()
        {
            return _information.Id;
        }

        public string GetCreationDate()
        {
            return _information.CreationDate.ToString();
        }

        public Note(string name = "New Note", string value = "", int rows = 10, int columns = 100)
        {
            _information = CreateNoteInformationData();
            _name = name;
            _value = value;
            _rows = rows;
            _columns = columns;
        }

        public NoteInformationRecord CreateNoteInformationData()
        {
            DateTime CreationDate = DateTime.Now;
            int Id = _idNumber++;

            return new NoteInformationRecord(CreationDate, Id);
        }

        public string Render()
        {
            return $"<textarea>{Value}</textarea>";
        }
    }
}
