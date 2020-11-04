namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    public interface ILoginMessage : IMessage
    {
        public string Login { get; }
        public string Password { get; }
    }
}