using System;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    public class OnLoginInput
    {
        internal OnLoginInput(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name.Value;
            this.Login = user.Email.Value;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Login { get; }
    }
}