using System;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    internal class UserLoggedInEvent
    {
        public UserLoggedInEvent(Guid userId, string name, string login)
        {
            this.Id = userId;
            this.Name = name;
            this.Login = login;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Login { get; set; }
    }
}