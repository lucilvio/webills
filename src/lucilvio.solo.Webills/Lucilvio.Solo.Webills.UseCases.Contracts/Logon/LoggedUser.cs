using System;
using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.Logon
{
    public class LoggedUser
    {
        public LoggedUser(string name, Login login)
        {
            this.Name = name;
            this.Login = login;
        }

        public string Name { get; }
        public Login Login { get; }
    }
}