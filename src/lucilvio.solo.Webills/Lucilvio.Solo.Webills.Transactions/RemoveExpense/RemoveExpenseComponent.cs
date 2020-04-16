using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    internal class RemoveExpenseComponent
    {
        private readonly IRemoveExpenseDataAccess _dataAccess;

        public RemoveExpenseComponent(IRemoveExpenseDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public async Task Execute(RemoveExpenseInput input, Func<RemovedExpense, Task> onRemoveExpense = null)
        {
            var foundUser = await this._dataAccess.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.RemoveExpense(input.ExpenseId);

            await this._dataAccess.Persist(input.ExpenseId);

            if (onRemoveExpense != null)
                onRemoveExpense.Invoke(new RemovedExpense(foundUser.Id, input.ExpenseId));
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}