namespace SoftwareEngineeringProject.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public ICollection<UserNotes> UserNotes { get; set; }
    }
}
