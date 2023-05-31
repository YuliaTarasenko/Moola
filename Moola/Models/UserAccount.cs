namespace Moola.Models
{
    public sealed record UserAccount
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public User User { get; set; }
        public Account Account { get; set; }
    }
}
