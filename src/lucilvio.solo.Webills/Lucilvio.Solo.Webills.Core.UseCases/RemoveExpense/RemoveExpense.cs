using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Domain.Shared;
using Lucilvio.Solo.Webills.UseCases.Shared.Errors;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.RemoveExpense;

namespace Lucilvio.Solo.Webills.Core.UseCases.RemoveExpense
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
            if (command.NotDefined())
                throw new CommandNotInformed();

            var user = await this._dataStorage.GetUserById(command.UserId);

            if (user.NotDefined())
                throw new UserNotFound();

            user.RemoveExpense(command.ExpenseId);

            await this._dataStorage.Persist(command.ExpenseId);
        }
    }
}