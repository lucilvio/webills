using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions.AddNewExpense
{
    internal class AddNewExpenseComponent
    {
        private readonly IAddNewExpenseDataAccess _dataAccess;

        public AddNewExpenseComponent(IAddNewExpenseDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task Execute(AddNewExpenseInput input, Func<CreatedExpense, Task> onExpenseCreate)
        {
            var foundUser = await this._dataAccess.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            var newExpense = foundUser.AddExpense(input.Name, input.Category, input.Date, input.Value);

            await this._dataAccess.Persist();

            if (onExpenseCreate != null)
                onExpenseCreate.Invoke(new CreatedExpense(foundUser, newExpense));
        }

        internal class Error
        {
            public class UserNotFound : Exception { }
        }
    }
}
