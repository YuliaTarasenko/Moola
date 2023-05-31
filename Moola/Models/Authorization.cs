namespace Moola.Models
{
    public sealed record Authorization : EntityWithId
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Authorization() { }
        public Authorization(int id,string login, string password, int userId) 
        { 
            Id = id;
            Login = login;
            Password = password;
            UserId = userId;
        }
    }
}
