namespace Moola.Models
{
    public sealed record Authorization : EntityWithId
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public List<User> Users { get; set; }
        public Authorization() { }
        public Authorization(int id,string login, string password) 
        { 
            Id = id;
            Login = login;
            Password = password;
        }
    }
}
