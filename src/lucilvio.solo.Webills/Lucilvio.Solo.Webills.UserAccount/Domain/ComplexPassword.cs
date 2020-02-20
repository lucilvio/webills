using System;

using Lucilvio.Solo.Webills.UserProfile.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    class ComplexPassword : IPassword
    {
        private readonly IPassword _password;

        public ComplexPassword(IPassword password)
        {
            if (password.Value.Length < 6)
                throw new Error.PasswordMustBeGreaterThan6Characters();

            this._password = password;
        }

        public string Value => this._password.Value;

        class Error
        {
            internal class PasswordMustBeGreaterThan6Characters : Exception { }
        }
    }
}
