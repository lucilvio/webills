using Lucilvio.Solo.Webills.UserProfile.Domain;

namespace Lucilvio.Solo.Webills.UserProfile.RegisterUser
{
    public abstract class RegisterUserCommand
    {
        public abstract Login Login { get; }
        public abstract Password Password { get; }
        public abstract Password PasswordConfirmation { get; }
        public abstract string Name { get; }
        public abstract bool TermsAccepted { get; }
    }
}