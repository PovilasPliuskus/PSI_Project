using System.Collections;

namespace SoftwareEngineeringProject.NoteLibrary
{
    public class NoteComparer : IComparer<Note>
    {
        private readonly ComparisonType _comparisonType;
        public enum ComparisonType
        {
            Name,
            CreationDate
        }

        public NoteComparer(ComparisonType comparisonType)
        {
            _comparisonType = comparisonType;
        }

        public int Compare(Note x, Note y)
        {
            if (_comparisonType == ComparisonType.Name)
            {
                return x.Name.CompareTo(y.Name);
            }
            else if (_comparisonType == ComparisonType.CreationDate)
            {
                return x.GetCreationDate().CompareTo(y.GetCreationDate());
            }
            else
            {
                throw new ArgumentException("Invalid comparison type.");
            }
        }
    }
}
