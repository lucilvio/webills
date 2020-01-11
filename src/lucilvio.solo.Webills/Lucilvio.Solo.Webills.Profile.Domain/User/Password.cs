using System;

using Lucilvio.Solo.Webills.Profile.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Profile.Domain.User
{
    public class Password
    {
        public Password(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new PasswordCannotBeNullOrEmpty();

            if (value.Length < 6)
                throw new PasswordMustBeGreaterThan6Characters();

            this.Value = value;
        }

        public string Value { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            return this.Value == ((Password)obj).Value;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static bool operator ==(Password password1, Password password2)
        {
            if (Object.ReferenceEquals(password1, password2))
                return true;

            if ((password1 is null) || (password2 is null))
                return false;

            return password1.Value == password2.Value;
        }

        public static bool operator !=(Password password1, Password password2)
        {
            return !(password1 == password2);
        }
    }
}