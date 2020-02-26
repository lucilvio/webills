using System;

namespace Lucilvio.Solo.Webills.Clients.Web.Login
{
    public class UserAuthCredentials
    {
        public UserAuthCredentials(Guid id, string login, string name)
        {
            Id = id;
            Login = login;
            Name = name;
        }

        public Guid Id { get; }
        public string Login { get; }
        public string Name { get; }
    }
}