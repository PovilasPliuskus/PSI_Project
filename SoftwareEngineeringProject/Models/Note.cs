using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json;
using SoftwareEngineeringProject.Enums;
namespace SoftwareEngineeringProject.Models
{
    public class Note
    {
        private Guid _id;
        private DateTime _creationDate;
        private String _name;
        private String _value;
        private NoteCategory _category;

        public ICollection<UserNotes> UserNotes { get; set; }

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

        public NoteCategory Category
        {
            get { return _category; }
            set { }
        }

        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string GetCreationDate()
        {
            return _creationDate.ToString();
        }

        public Note(string name = "New Note", string value = "", NoteCategory category = NoteCategory.Personal, string userId = null)
        {
            _id = Guid.NewGuid();
            _creationDate = DateTime.Now;
            _name = name;
            _value = value;
            _category = category;
            UserId = userId;
        }

        public void ToString()
        {
            Console.WriteLine("id: " + Id);
            Console.WriteLine("name: " + Name);
            Console.WriteLine("value: " + Value);
            Console.WriteLine("creation date: " + GetCreationDate());
            Console.WriteLine("category: " + Category);
            Console.WriteLine();
        }
    }
}
