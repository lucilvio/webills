using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.EditIncome
{
    internal class EditIncomeUseCase : IEditIncomeUseCase
    {
        private readonly IEditIncomeDataAccess _dataStorage;

        public EditIncomeUseCase(IEditIncomeDataAccess dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public async Task Execute(EditIncomeCommand command)
        {
            if (command == null)
                throw new Error.CommandNotInformed();

            var foundUser = await _dataStorage.GetUserById(command.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.EditIncome(command.Id, command.Name, command.Date, new TransactionValue(command.Value));

            await _dataStorage.Persist();
        }

        internal class Error
        {
            internal class CommandNotInformed : Exception { }
            internal class UserNotFound : Exception { }
        }
    }
}