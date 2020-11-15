using System;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    internal record Login
    {
        public Login(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Error.LoginCannotBeEmpty();

            this.Value = new Email(value).Value;
        }

        public string Value { get; }

        internal class Error
        {
            internal class LoginCannotBeEmpty : Exception { }
        }
    }
}