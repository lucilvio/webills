namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    public class LoginInput
    {
        public LoginInput(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }

        public string Login { get; }
        public string Password { get; }
    }
}