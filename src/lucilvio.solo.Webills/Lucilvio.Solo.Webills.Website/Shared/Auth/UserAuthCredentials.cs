using System;

namespace Lucilvio.Solo.Webills.Website.Shared.Authorization
{
    public class UserAuthCredentials
    {
        public UserAuthCredentials(Guid id, string name, string login)
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