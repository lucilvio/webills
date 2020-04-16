namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    public class LoginInput
    {
        public LoginInput(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }

        internal string Login { get; }
        internal string Password { get; }
    }
}