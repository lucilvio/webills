using System;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    internal record ComplexPassword : IPassword
    {
        private readonly IPassword _password;

        public ComplexPassword(IPassword password)
        {
            if(password == null)
                throw new Error.PasswordCantBeEmpty();

            if (password.Value.Length < 6)
                throw new Error.PasswordMustBeGreaterThanSixCharacters();

            this._password = password;
        }

        public string Value => this._password.Value;

        internal class Error
        {
            internal class PasswordCantBeEmpty : Exception { }
            internal class PasswordMustBeGreaterThanSixCharacters : Exception { }
        }
    }
}
