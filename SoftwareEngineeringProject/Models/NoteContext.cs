using Microsoft.EntityFrameworkCore;

namespace SoftwareEngineeringProject.Models
{
    public class NoteContext : DbContext
    {
        public DbSet<Note> Notes { get; set;}

        public NoteContext(DbContextOptions options) : base(options)
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
