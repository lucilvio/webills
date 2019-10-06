using System.Collections.Generic;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.Tests;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class DataStorageContext
    {
        public IList<User> Users { get; set; }

        public DataStorageContext()
        {
            this.Users = new List<User>();
            this.Users.Add(new User("Test User"));
        }
    }
}