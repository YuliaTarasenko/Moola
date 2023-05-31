namespace Moola.Models
{
    public sealed record User: EntityWithId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public int AuthorizationId { get; set; }
        public Authorization Authorization { get; set; }
        public List<Account> Accounts { get; set; }
        public User() { }
        public User (int id, string firstName, string lastName, string email, DateTime birthday)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Birthday = birthday;
        }
    }
}
