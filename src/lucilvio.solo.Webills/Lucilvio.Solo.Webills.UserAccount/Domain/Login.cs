using System;
using System.Text.RegularExpressions;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    internal class Login
    {
        public Login(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Error.LoginCannotBeEmpty();

            this.Value = new Email(value).Value;
        }

        public string Value { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Value == ((Login)obj).Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Login login1, Login login2)
        {
            if (ReferenceEquals(login1, login2))
                return true;

            if (login1 is null || login2 is null)
                return false;

            return login1.Value == login2.Value;
        }

        public static bool operator !=(Login login1, Login login2)
        {
            return !(login1 == login2);
        }

        internal class Error
        {
            internal class LoginCannotBeEmpty : Exception { }
        }
    }
}