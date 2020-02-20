using System.Security.Cryptography;
using System.Text;

namespace Lucilvio.Solo.Webills.UserProfile.Domain
{
    public class Sha1EncryptedPassword : IPassword
    {
        private readonly IPassword _password;

        public Sha1EncryptedPassword(IPassword password) : base()
        {
            _password = password;
        }

        public string Value { get => this.Encrypt(_password.Value); }

        private string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(value));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                    sb.Append(b.ToString("X2"));

                return sb.ToString();
            }
        }
    }
}