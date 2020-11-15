using System;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    internal record Password : IPassword
    {
        public Password(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Error.PasswordCantBeNullOrEmpty();

            Value = value;
        }

        public virtual string Value { get; }

        internal class Error
        {
            internal class PasswordCantBeNullOrEmpty : Exception { }
        }
    }
}