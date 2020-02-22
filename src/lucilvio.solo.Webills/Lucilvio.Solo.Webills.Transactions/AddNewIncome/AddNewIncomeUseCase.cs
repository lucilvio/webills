using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.AddNewIncome
{
    internal class AddNewIncomeUseCase : IUseCase<AddNewIncomeCommand>
    {
        private readonly IAddNewIncomeDataAccess _dataStorage;

        public AddNewIncomeUseCase(IAddNewIncomeDataAccess dataStorage)
        {
            _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
        }

        public async Task Execute(AddNewIncomeCommand command)
        {
            if (command == null)
                throw new Error.CommandNotInformed();

            var foundUser = await _dataStorage.GetUserById(command.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.AddIncome(command.Name, command.Date, new TransactionValue(command.Value));

            await _dataStorage.Persist();
        }

        internal class Error
        {
            internal class CommandNotInformed : Exception { }
            internal class UserNotFound : Exception { }
        }
    }
}