using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.AddNewExpense
{
    internal class AddNewExpenseMessageHandler
    {
        private readonly IEventBus _bus;
        private readonly IAddNewExpenseDataAccess _dataAccess;

        public AddNewExpenseMessageHandler(IAddNewExpenseDataAccess dataAccess, IEventBus bus)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            this._bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task Execute(IAddNewExpenseMessage message)
        {
            var foundUser = await this._dataAccess.GetUserById(message.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            if (!Enum.TryParse(typeof(Expense.ExpenseCategory), message.Category, out var category))
                category = Expense.ExpenseCategory.Others;

            var newExpense = foundUser.AddExpense(message.Name, (Expense.ExpenseCategory)category, message.Date, new TransactionValue(message.Value));

            await this._dataAccess.Persist();

            this._bus.Publish("NewExpenseAdded", new OnAddExpenseInput(foundUser, newExpense));
        }

        internal class Error
        {
            public class UserNotFound : Exception { }
        }
    }
}