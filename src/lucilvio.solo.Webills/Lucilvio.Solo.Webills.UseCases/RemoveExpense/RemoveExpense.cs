using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UseCases.Common;
using Lucilvio.Solo.Webills.UseCases.Contracts.RemoveExpense;

namespace Lucilvio.Solo.Webills.UseCases.RemoveExpense
{
    public class RemoveExpense : IRemoveExpense
    {
        private readonly IRemoveExpenseDataStorage _dataStorage;

        public RemoveExpense(IRemoveExpenseDataStorage removeExpenseDataStorage)
        {
            this._dataStorage = removeExpenseDataStorage;
        }

        public async Task Execute(RemoveExpenseCommand command)
        {
            if (command == null)
                throw new CommandNotInformed();

            var foundUser = await this._dataStorage.GetUser();

            if (foundUser == null)
                throw new UserNotFound();

            foundUser.RemoveExpense(command.ExpenseNumber);

            await this._dataStorage.Persist();
        }
    }
}