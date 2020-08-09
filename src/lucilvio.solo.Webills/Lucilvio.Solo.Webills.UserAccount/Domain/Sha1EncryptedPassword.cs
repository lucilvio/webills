using System.Security.Cryptography;
using System.Text;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    internal class Sha1EncryptedPassword : IPassword
    {
        private readonly IPassword _password;

        public Sha1EncryptedPassword(IPassword password) : base()
        {
            _password = password;
        }

        public string Value { get => Encrypt(_password.Value); }

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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Value == ((IPassword)obj).Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(IPassword password1, Sha1EncryptedPassword password2)
        {
            if (ReferenceEquals(password1, password2))
                return true;

            if (password1 is null || password2 is null)
                return false;

            return password1.Value == password2.Value;
        }

        public static bool operator !=(IPassword password1, Sha1EncryptedPassword password2)
        {
            return !(password1 == password2);
        }
    }
}