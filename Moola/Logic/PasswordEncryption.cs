namespace Moola.Logic
{
    public class PasswordEncryption
    {
        //Hashing password
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create() ;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashedBytes = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashedBytes);
        }

        //Checking password
        public static bool CheckPassword(string password, string hashedPassword)=> HashPassword(password) == hashedPassword;
    }
}
