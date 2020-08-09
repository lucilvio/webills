using System;
using System.Text.RegularExpressions;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    internal class Email
    {
        public Email(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Error.EmailCannotBeEmpty();

            if (!this.IsInValidFormat(value))
                throw new Error.InvalidEmailFormat();

            this.Value = value;
        }

        public string Value { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Value == ((Email)obj).Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Email email1, Email email2)
        {
            if (ReferenceEquals(email1, email2))
                return true;

            if (email1 is null || email2 is null)
                return false;

            return email1.Value == email2.Value;
        }

        public static bool operator !=(Email email1, Email email2)
        {
            return !(email1 == email2);
        }

        private bool IsInValidFormat(string value)
        {
            var emailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
              + "@"
              + @"((([\w]+([-\w]*[\w]+)*\.)+[a-zA-Z]+)|"
              + @"((([01]?[0-9]{1,2}|2[0-4][0-9]|25[0-5]).){3}[01]?[0-9]{1,2}|2[0-4][0-9]|25[0-5]))\z";

            var emailRegex = new Regex(emailPattern);

            return emailRegex.IsMatch(value);
        }

        internal class Error
        {
            internal class EmailCannotBeEmpty : Exception { }
            internal class InvalidEmailFormat : Exception { }
        }
    }
}