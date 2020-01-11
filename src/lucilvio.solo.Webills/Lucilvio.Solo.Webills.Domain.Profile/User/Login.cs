using Lucilvio.Solo.Webills.Domain.Profile.User.BusinessErrors;
using System;
using System.Text.RegularExpressions;

namespace Lucilvio.Solo.Webills.Domain.Profile.User
{
    public class Login
    {
        public Login(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new LoginCannotBeNullOrEmpty();

            if (!this.IsAValidEmail(value))
                throw new LoginIsNotAValidEmail();

            this.Value = value;
        }

        public string Value { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            return this.Value == ((Login)obj).Value;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static bool operator ==(Login login1, Login login2)
        {
            if (Object.ReferenceEquals(login1, login2))
                return true;

            if ((login1 is null) || (login2 is null))
                return false;

            return login1.Value == login2.Value;
        }

        public static bool operator !=(Login login1, Login login2)
        {
            return !(login1 == login2);
        }

        private bool IsAValidEmail(string value)
        {
            var emailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
              + "@"
              + @"((([\w]+([-\w]*[\w]+)*\.)+[a-zA-Z]+)|"
              + @"((([01]?[0-9]{1,2}|2[0-4][0-9]|25[0-5]).){3}[01]?[0-9]{1,2}|2[0-4][0-9]|25[0-5]))\z";

            var emailRegex = new Regex(emailPattern);

            return emailRegex.IsMatch(value);
        }
    }
}