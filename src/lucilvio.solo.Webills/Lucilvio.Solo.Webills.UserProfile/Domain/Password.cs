using Lucilvio.Solo.Webills.UserProfile.Domain.BusincessErrors;

namespace Lucilvio.Solo.Webills.UserProfile.Domain
{
    public class Password : IPassword
    {
        public Password(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new PasswordCannotBeNullOrEmpty();

            if (value.Length < 6)
                throw new PasswordMustBeGreaterThan6Characters();

            Value = value;
        }

        public virtual string Value { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Value == ((Password)obj).Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Password password1, Password password2)
        {
            if (ReferenceEquals(password1, password2))
                return true;

            if (password1 is null || password2 is null)
                return false;

            return password1.Value == password2.Value;
        }

        public static bool operator !=(Password password1, Password password2)
        {
            return !(password1 == password2);
        }
    }
}