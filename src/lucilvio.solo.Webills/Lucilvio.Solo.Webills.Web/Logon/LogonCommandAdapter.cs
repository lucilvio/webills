using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.Contracts.Logon;

namespace Lucilvio.Solo.Webills.Web.Logon
{
    internal class LogonCommandAdapter : LogonCommand
    {
        public LogonCommandAdapter(LogonRequest request)
        {
            if (request == null)
                return;

            base.Login = request.Login;
            base.Password = request.Password;
        }
    }
}