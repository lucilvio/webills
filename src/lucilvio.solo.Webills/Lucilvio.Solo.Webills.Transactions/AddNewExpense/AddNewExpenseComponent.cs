using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions.AddNewExpense
{
    internal class AddNewExpenseComponent
    {
        private readonly IBusSender _bus;
        private readonly IAddNewExpenseDataAccess _dataAccess;

        public AddNewExpenseComponent(IAddNewExpenseDataAccess dataAccess, IBusSender bus)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            this._bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task Execute(AddNewExpenseInput input)
        {
            var foundUser = await this._dataAccess.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            var newExpense = foundUser.AddExpense(input.Name, input.Category, input.Date, input.Value);

            await this._dataAccess.Persist();

            this._bus.SendEvent(new OnAddExpenseInput(foundUser, newExpense));
        }

        internal class Error
        {
            public class UserNotFound : Exception { }
        }
    }
}