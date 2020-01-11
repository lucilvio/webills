using System;

namespace Lucilvio.Solo.Webills.Domain.Security.User
{
    public class User
    {
        private User()
        {
        }

        public User(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }

        public Guid Id { get; private set; }
        
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
    }
}