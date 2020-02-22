namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    public abstract class LoginCommand : ICommand
    {
        public abstract string Login { get; }
        public abstract string Password { get; }
    }
}