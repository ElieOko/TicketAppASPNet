using System.Security.Cryptography;
using System.Text;

namespace TicketApp.Services
{
    public class PaaswordHasher
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedSalt == null || storedSalt.Length == 0)
            {
                throw new ArgumentNullException(nameof(storedSalt), "Le sel ne peut pas être nul ou vide.");
            }
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}



