using System;

namespace Lucilvio.Solo.Webills.Clients.Web.Shared.Authentication
{
    public class AuthenticatedUser
    {
        public AuthenticatedUser(Guid id, string login, string name)
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