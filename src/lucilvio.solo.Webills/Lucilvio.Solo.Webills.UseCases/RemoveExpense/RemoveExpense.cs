using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.Shared;
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

            var user = await this._dataStorage.GetUserById(command.UserId);

            if (user.NotDefined())
                throw new UserNotFound();

            user.RemoveExpense(command.ExpenseId);

            await this._dataStorage.Persist(command.ExpenseId);
        }
    }
}