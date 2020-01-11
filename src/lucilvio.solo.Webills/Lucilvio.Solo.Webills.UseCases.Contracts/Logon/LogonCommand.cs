namespace Lucilvio.Solo.Webills.UseCases.Contracts.Logon
{
    public abstract class LogonCommand
    {
        public string Login { get; protected set; }
        public string Password { get; protected set; }
    }
}
