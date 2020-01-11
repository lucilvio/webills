using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UseCases.Common;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditExpense;

namespace Lucilvio.Solo.Webills.UseCases.EditExpense
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
            if (command == null)
                throw new CommandNotInformed();

            var foundUser = await this._dataStorage.GetUser();

            if (foundUser == null)
                throw new UserNotFound();

            foundUser.EditExpense(command.Id, command.Name, command.Category, command.Date, command.Value);

            await this._dataStorage.Persist(command.Id, foundUser);

        }
    }
}