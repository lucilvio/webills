using System;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    internal class Name
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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Value == ((Name)obj).Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Name name1, Name name2)
        {
            if (ReferenceEquals(name1, name2))
                return true;

            if (name1 is null || name2 is null)
                return false;

            return name1.Value == name2.Value;
        }

        public static bool operator !=(Name name1, Name name2)
        {
            return !(name1 == name2);
        }

        internal class Error
        {
            internal class NameCannotBeEmpty : Exception { }
            internal class NameIsTooLong : Exception { }
        }
    }
}