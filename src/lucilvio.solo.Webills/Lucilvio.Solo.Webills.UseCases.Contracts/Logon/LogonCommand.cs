using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.Logon
{
    public abstract class LogonCommand
    {
        public Login Login { get; protected set; }
        public Password Password { get; protected set; }
    }
}
