namespace Reto.Application.Password
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return $"{salt}-¿-{hashedPassword}";
        }

        public static bool VerifyPassword(string password, string hashedPasswordWithSalt)
        {
            var parts = hashedPasswordWithSalt.Split("-¿-");
            if (parts.Length != 2)
            {
                // El formato del hash no es válido
                return false;
            }

            var salt = parts[0];
            var hashedPassword = parts[1];

            string hashedInputPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedInputPassword == hashedPassword;
        }
    }
}
