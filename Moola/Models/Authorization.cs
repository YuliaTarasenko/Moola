namespace Moola.Models
{
    public sealed record Authorization : EntityWithId
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
