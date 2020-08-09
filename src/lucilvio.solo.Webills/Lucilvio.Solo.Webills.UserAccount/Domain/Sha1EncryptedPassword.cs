using System;
using System.Security.Cryptography;
using System.Text;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    internal class Sha1EncryptedPassword : IPassword
    {
        private readonly IPassword _password;

        public Sha1EncryptedPassword(IPassword password) : base()
        {
            if (password == null)
                throw new Error.PasswordCantBeEmpty();

            this._password = password;
        }

        public string Value { get => this.Encrypt(this._password.Value); }

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
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            return this.Value == ((IPassword)obj).Value;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static bool operator ==(Sha1EncryptedPassword password1, IPassword password2)
        {
            if (ReferenceEquals(password1, password2))
                return true;

            if (password1 is null || password2 is null)
                return false;

            return password1.Value == password2.Value;
        }

        public static bool operator !=(Sha1EncryptedPassword password1, IPassword password2)
        {
            return !(password1 == password2);
        }

        internal class Error
        {
            public class PasswordCantBeEmpty : Exception { }
        }
    }
}