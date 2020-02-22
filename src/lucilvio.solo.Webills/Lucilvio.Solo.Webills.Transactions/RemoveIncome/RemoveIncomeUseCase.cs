using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions.RemoveExpense;

namespace Lucilvio.Solo.Webills.Transactions.RemoveIncome
{
    internal class RemoveIncomeUseCase : IUseCase<RemoveIncomeCommand>
    {
        private readonly IRemoveIncomeDataAccess _dataAccess;

        public RemoveIncomeUseCase(IRemoveIncomeDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task Execute(RemoveIncomeCommand command)
        {
            if (command == null)
                throw new Error.CommandNotInformed();

            var foundUser = await _dataAccess.GetUserById(command.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.RemoveIncome(command.IncomeId);

            await _dataAccess.Persist(command.IncomeId);
        }

        internal class Error
        {
            internal class CommandNotInformed : Exception { }
            internal class UserNotFound : Exception { }
        }
    }
}