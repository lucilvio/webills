using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.RemoveExpense;
using Lucilvio.Solo.Webills.Shared.Domain;
using Lucilvio.Solo.Webills.Shared.UseCases.Errors;

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