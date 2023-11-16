using Microsoft.EntityFrameworkCore;

namespace SoftwareEngineeringProject.Models
{
    public class NoteDBContext : DbContext
    {
        public DbSet<Note> Notes { get; set;}

        // NoteDBContext
        public NoteDBContext(DbContextOptions options) : base(options)
        {
            
        }
/*        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>()
                .HasKey(n => n.Id);
        }*/
    }
}
