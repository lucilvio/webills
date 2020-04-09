using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.AddNewExpense
{
    internal class AddNewExpenseComponent
    {
        private readonly IAddNewExpenseDataAccess _dataAccess;

        public AddNewExpenseComponent(IAddNewExpenseDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new System.ArgumentNullException(nameof(dataAccess));
        }

        public async Task Execute(IAddNewExpenseInput input, Func<NewAddedExpense, Task> onAddExpense)
        {
            var foundUser = await this._dataAccess.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            var newExpense = 
                foundUser.AddExpense(input.Name, Enum.Parse<Category>(input.Category, true), input.Date, new TransactionValue(input.Value));

            await this._dataAccess.Persist();

            if (onAddExpense != null)
                await onAddExpense.Invoke(new NewAddedExpense(foundUser.Id, newExpense));
        }

        internal class Error
        {
            public class UserNotFound : Exception { }
        }
    }
}
