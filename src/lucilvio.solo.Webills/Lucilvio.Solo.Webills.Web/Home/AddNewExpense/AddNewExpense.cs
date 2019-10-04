using Lucilvio.Solo.Webills.Tests;
using Lucilvio.Solo.Webills.Web.Home;

namespace Lucilvio.Solo.Webills.Web
{
    internal class AddNewExpense : IAddNewExpense
    {
        private readonly IAddNewExpenseDataStorage _dataStorage;

        public AddNewExpense(IAddNewExpenseDataStorage dataStorage)
        {
            this._dataStorage = dataStorage;
        }

        public void Execute(AddNewExpenseCommandAdapter command)
        {
            var user = new User();
            user.AddExpense(new Expense(command.Name, command.Date, command.Value));

            this._dataStorage.AddUserExpenseData(user);
        }
    }
}