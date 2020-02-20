using System;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    public class LoggedUser
    {
        public LoggedUser(Guid userId, string name, string login)
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