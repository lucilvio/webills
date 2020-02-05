using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.EditExpense;
using Lucilvio.Solo.Webills.Shared.Domain;
using Lucilvio.Solo.Webills.Shared.UseCases.Errors;

namespace Lucilvio.Solo.Webills.Core.UseCases.EditExpense
{
    public class EditExpense : IEditExpense
    {
        private readonly IEditExpenseDataStorage _dataStorage;

        public EditExpense(IEditExpenseDataStorage dataStorage)
        {
            this._dataStorage = dataStorage;
        }

        public async Task Execute(EditExpenseCommand command)
        {
            if (command.NotDefined())
                throw new CommandNotInformed();

            var foundUser = await this._dataStorage.GetUser();

            if (foundUser.NotDefined())
                throw new UserNotFound();

            foundUser.EditExpense(command.Id, command.Name, command.Category, command.Date, command.Value);

            await this._dataStorage.Persist(command.Id, foundUser);

        }
    }
}