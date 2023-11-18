using Microsoft.EntityFrameworkCore;

namespace SoftwareEngineeringProject.Models
{
    public class NoteDBContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserNotes> UserNotes { get; set; }

        public NoteDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserNotes>()
                .HasKey(un => new { un.NoteId, un.UserId });

            modelBuilder.Entity<UserNotes>()
                .HasOne(un => un.User)
                .WithMany(u => u.UserNotes)
                .HasForeignKey(un => un.UserId);

            modelBuilder.Entity<UserNotes>()
                .HasOne(un => un.Note)
                .WithMany(n => n.UserNotes)
                .HasForeignKey(un => un.NoteId);
        }
    }
}
