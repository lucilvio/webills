using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.Web.Logon
{
    public class Credentials
    {
        public Credentials(string login, string name)
        {
            this.Login = login;
            this.Name = name;
        }

        public string Login { get; }
        public string Name { get; }
    }
}