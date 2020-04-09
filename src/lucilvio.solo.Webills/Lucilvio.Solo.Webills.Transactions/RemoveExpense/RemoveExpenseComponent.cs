using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    internal class RemoveExpenseComponent
    {
        private readonly IRemoveExpenseDataAccess _dataAccess;

        public RemoveExpenseComponent(IRemoveExpenseDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task Execute(RemoveExpenseInput input)
        {
            var foundUser = await _dataAccess.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.RemoveExpense(input.ExpenseId);

            await _dataAccess.Persist(input.ExpenseId);
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}