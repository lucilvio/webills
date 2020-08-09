using System;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    internal class Password : IPassword
    {
        public Password(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Error.PasswordCantBeNullOrEmpty();

            Value = value;
        }

        public virtual string Value { get; }

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

        public static bool operator ==(Password password1, IPassword password2)
        {
            if (ReferenceEquals(password1, password2))
                return true;

            if (password1 is null || password2 is null)
                return false;

            return password1.Value == password2.Value;
        }

        public static bool operator !=(Password password1, IPassword password2)
        {
            return !(password1 == password2);
        }

        internal class Error
        {
            internal class PasswordCantBeNullOrEmpty : Exception { }
        }
    }
}