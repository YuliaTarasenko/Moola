namespace Moola.Models
{
    public sealed record User : EntityWithId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public Account Account { get; set; }
        public List<Finance> Finances { get; set; }
    }
}