using System.Linq;
using System.Collections.Generic;
using Lucilvio.Solo.Webills.Tests;
using Lucilvio.Solo.Webills.Web.Home;

namespace Lucilvio.Solo.Webills.Web
{
    internal class AddNewIcomeDataStorageInMemory : IAddNewIncomeDataStorage
    {
        public IList<User> Users { get; set; }

        public AddNewIcomeDataStorageInMemory()
        {
            this.Users = new List<User>();
            this.Users.Add(new User());
        }

        public void AddUserIncomeData(User user)
        {
            var foundUser = this.Users.FirstOrDefault();

            if (foundUser == null)
                return;

            foundUser.AddIncome(user.Incomes.FirstOrDefault());
        }
    }
}