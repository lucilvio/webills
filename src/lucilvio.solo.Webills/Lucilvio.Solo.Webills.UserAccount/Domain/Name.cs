using System;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    internal record Name
    {
        public Name(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Error.NameCannotBeEmpty();

            if (value.Length > 256)
                throw new Error.NameIsTooLong();

            this.Value = value;
        }

        public string Value { get; }

        internal class Error
        {
            internal class NameCannotBeEmpty : Exception { }
            internal class NameIsTooLong : Exception { }
        }
    }
}