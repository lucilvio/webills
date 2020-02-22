using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.EditExpense
{
    internal class EditExpenseUseCase : IUseCase<EditExpenseCommand>
    {
        private readonly IEditExpenseDataAccess _dataStorage;

        public EditExpenseUseCase(IEditExpenseDataAccess dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public async Task Execute(EditExpenseCommand command)
        {
            if (command == null)
                throw new Error.CommandNotInformed();

            var foundUser = await _dataStorage.GetUserById(command.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.EditExpense(command.Id, command.Name, Enum.Parse<Category>(command.Category, true), command.Date,
                new TransactionValue(command.Value));

            await _dataStorage.Persist();
        }

        internal class Error
        {
            internal class CommandNotInformed : Exception { }
            internal class UserNotFound : Exception { }
        }
    }
}