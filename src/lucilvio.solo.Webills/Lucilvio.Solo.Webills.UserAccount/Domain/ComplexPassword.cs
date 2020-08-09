using System;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    class ComplexPassword : IPassword
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

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Value == ((IPassword)obj).Value;
        }

        public static bool operator ==(ComplexPassword password1, IPassword password2)
        {
            if (ReferenceEquals(password1, password2))
                return true;

            if (password1 is null || password2 is null)
                return false;

            return password1.Value == password2.Value;
        }

        public static bool operator !=(ComplexPassword password1, IPassword password2)
        {
            return !(password1 == password2);
        }

        internal class Error
        {
            internal class PasswordCantBeEmpty : Exception { }
            internal class PasswordMustBeGreaterThanSixCharacters : Exception { }
        }
    }
}
