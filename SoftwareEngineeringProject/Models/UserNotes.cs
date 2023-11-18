using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareEngineeringProject.Models
{
    public class UserNotes
    {
        [Key]
        public Guid NoteId { get; set; }
        [ForeignKey("NoteId")]
        public Note Note { get; set; }

        [Key]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
