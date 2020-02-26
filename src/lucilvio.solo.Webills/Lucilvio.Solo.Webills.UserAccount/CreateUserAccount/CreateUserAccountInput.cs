namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    public interface ICreateUserAccountInput
    {
        public string Login { get; }
        public string Password { get; }
        public string PasswordConfirmation { get; }
        public string Name { get; }
        public string Email { get; }
        public bool TermsAccepted { get; }
    }
}