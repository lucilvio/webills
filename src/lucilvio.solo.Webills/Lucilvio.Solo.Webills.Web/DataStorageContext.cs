using System.Collections.Generic;
using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class DataStorageContext
    {
        public IList<User> Users { get; set; }

        public DataStorageContext()
        {
            this.Users = new List<User>();
            Users.Add(new User("Test User", new Login("user@mail.com"), new Password("123456")));
        }
    }
}