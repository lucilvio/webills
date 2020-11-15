using System;
using System.Text;
using System.Security.Cryptography;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    internal record Sha1EncryptedPassword : IPassword
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

        internal class Error
        {
            public class PasswordCantBeEmpty : Exception { }
        }
    }
}