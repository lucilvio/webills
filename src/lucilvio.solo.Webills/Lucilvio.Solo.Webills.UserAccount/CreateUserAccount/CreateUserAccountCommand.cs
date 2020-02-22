namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    public abstract class CreateUserAccountCommand : ICommand
    {
        public abstract string Login { get; }
        public abstract string Password { get; }
        public abstract string PasswordConfirmation { get; }
        public abstract string Name { get; }
        public abstract string Email { get; }
        public abstract bool TermsAccepted { get; }
    }
}