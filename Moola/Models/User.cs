namespace Moola.Models
{
    public sealed record User : EntityWithId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public Authorization Authorization { get; set; }
        public List<Account> Accounts { get; set; }
    }
}