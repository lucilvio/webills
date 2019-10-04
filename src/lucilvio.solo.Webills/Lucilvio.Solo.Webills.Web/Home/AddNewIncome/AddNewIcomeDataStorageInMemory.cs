using System.Linq;
using System.Collections.Generic;
using Lucilvio.Solo.Webills.Tests;
using Lucilvio.Solo.Webills.Web.Home;

namespace Lucilvio.Solo.Webills.Web
{
    internal class AddNewIcomeDataStorageInMemory : IAddNewIncomeDataStorage
    {
        private readonly DataStorageContext _context;

        public AddNewIcomeDataStorageInMemory(DataStorageContext context)
        {
            this._context = context;
        }

        public void AddUserIncomeData(User user)
        {
            var foundUser = this._context.Users.FirstOrDefault();

            if (foundUser == null)
                return;

            foundUser.AddIncome(user.Incomes.LastOrDefault());
        }
    }
}