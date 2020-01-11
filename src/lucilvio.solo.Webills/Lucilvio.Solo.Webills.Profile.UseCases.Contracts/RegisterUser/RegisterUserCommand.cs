using Lucilvio.Solo.Webills.Profile.Domain.User;

namespace Lucilvio.Solo.Webills.Profile.UseCases.Contracts.RegisterUser
{
    public abstract class RegisterUserCommand
    {
        protected RegisterUserCommand(Login login, Password password, Password passwordConfirmation, string name, bool termsAccepted)
        {
            this.Login = login;
            this.Password = password;
            this.PasswordConfirmation = passwordConfirmation;
            this.Name = name;
            this.TermsAccepted = termsAccepted;
        }

        public Login Login { get; }
        public Password Password { get; }
        public Password PasswordConfirmation { get; }
        public string Name { get; }
        public bool TermsAccepted { get; set; }
    }
}