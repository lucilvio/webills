namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    class CreateUserAccountInput
    {
        public CreateUserAccountInput(string login, string password, string passwordConfirmation, string name, 
            string email, bool termsAccepted)
        {
            this.Login = login;
            this.Password = password;
            this.PasswordConfirmation = passwordConfirmation;
            this.Name = name;
            this.Email = email;
            this.TermsAccepted = termsAccepted;
        }

        internal string Login { get; }
        internal string Password { get; }
        internal string PasswordConfirmation { get; }
        internal string Name { get; }
        internal string Email { get; }
        internal bool TermsAccepted { get; }
    }
}