namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    public class CreateUserAccountInput
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

        public string Login { get; }
        public string Password { get; }
        public string PasswordConfirmation { get; }
        public string Name { get; }
        public string Email { get; }
        public bool TermsAccepted { get; }
    }
}