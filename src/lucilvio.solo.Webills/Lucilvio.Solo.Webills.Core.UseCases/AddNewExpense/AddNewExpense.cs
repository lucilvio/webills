using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Domain.Shared;
using Lucilvio.Solo.Webills.UseCases.Shared.Errors;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.AddNewExpense;

namespace Lucilvio.Solo.Webills.Core.UseCases.AddNewExpense
{
    public class AddNewExpense : IAddNewExpense
    {
        private readonly IAddNewExpenseDataStorage _dataStorage;

        public AddNewExpense(IAddNewExpenseDataStorage dataStorage)
        {
            this._dataStorage = dataStorage;
        }

        public async Task Execute(AddNewExpenseCommand command)
        {
            if (command.NotDefined())
                throw new CommandNotInformed();

            var user = await this._dataStorage.GetUserById(command.UserId);

            if (user.NotDefined())
                throw new UserNotFound();

            user.AddExpense(command.Name, command.Category, command.Date, command.Value);

            await this._dataStorage.Persist(user);
        }
    }
}