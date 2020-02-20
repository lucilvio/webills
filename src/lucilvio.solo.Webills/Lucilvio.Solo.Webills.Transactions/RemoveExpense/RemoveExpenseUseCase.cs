using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    internal class RemoveExpenseUseCase : IRemoveExpenseUseCase
    {
        private readonly IRemoveIncomeDataAccess _dataAccess;

        public RemoveExpenseUseCase(IRemoveIncomeDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task Execute(RemoveExpenseCommand command)
        {
            if (command == null)
                throw new Error.CommandNotInformed();

            var foundUser = await _dataAccess.GetUserById(command.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.RemoveExpense(command.ExpenseId);

            await _dataAccess.Persist(command.ExpenseId);
        }

        internal class Error
        {
            internal class CommandNotInformed : Exception { }
            internal class UserNotFound : Exception { }
        }
    }
}