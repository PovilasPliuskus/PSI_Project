using Microsoft.EntityFrameworkCore;

namespace SoftwareEngineeringProject.Models
{
    public class NoteDBContext : DbContext
    {
        public DbSet<Note> Notes { get; set;}

        public NoteDBContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
