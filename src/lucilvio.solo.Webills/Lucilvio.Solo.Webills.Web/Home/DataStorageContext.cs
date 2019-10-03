using Lucilvio.Solo.Webills.Tests;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class DataStorageContext
    {
        public IList<User> Users { get; set; }

        public DataStorageContext()
        {
            this.Users = new List<User>();
        }
    }
}