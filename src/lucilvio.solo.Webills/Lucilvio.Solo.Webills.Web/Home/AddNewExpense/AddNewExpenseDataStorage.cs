using Lucilvio.Solo.Webills.Tests;
using Lucilvio.Solo.Webills.Web.Home;
using System.Linq;

namespace Lucilvio.Solo.Webills.Web
{
    public class AddNewExpenseDataStorage : IAddNewExpenseDataStorage
    {
        private readonly DataStorageContext _dataStorage;

        public AddNewExpenseDataStorage(DataStorageContext dataStorage)
        {
            this._dataStorage = dataStorage;
        }

        public void AddUserExpenseData(User user)
        {
            var foundUser = this._dataStorage.Users.FirstOrDefault();

            if (foundUser == null)
                return;

            foundUser.AddExpense(user.Expenses.LastOrDefault());
        }
    }
}