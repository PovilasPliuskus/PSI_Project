namespace SoftwareEngineeringProject.NoteLibrary
{
    public class Note
    {
        private String id;
        private String name;
        private String value;
        private int rows;
        private int columns;

        public String ID
        {
            get { return id; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String Value
        {
            get { return value; }
            set { this.value = value.ToString(); }
        }

        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }

        public int Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        public Note(string id, string name, string value, int rows, int columns)
        {
            this.id = id;
            this.name = name;
            this.value = value;
            this.rows = rows;
            this.columns = columns;
        }

        public string Render()
        {
            return $"<textarea>{Value}</textarea>";
        }
    }
}
