using System;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    public class LoggedUser
    {
        internal LoggedUser(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name.Value;
            this.Login = user.Login.Value;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Login { get; }
    }
}