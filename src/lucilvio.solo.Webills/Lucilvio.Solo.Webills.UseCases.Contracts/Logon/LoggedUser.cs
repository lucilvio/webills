using System;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.Logon
{
    public class LoggedUser
    {
        public LoggedUser(Guid id, string name, string login)
        {
            this.Id = id;
            this.Name = name;
            this.Login = login;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Login { get; }
    }
}