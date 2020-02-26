using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    internal class RemoveExpenseComponent : IComponent
    {
        private readonly IRemoveIncomeDataAccess _dataAccess;

        public RemoveExpenseComponent(IRemoveIncomeDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task Execute(IRemoveExpenseInput input)
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