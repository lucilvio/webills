using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain;
using Lucilvio.Solo.Webills.Domain.Shared;
using Lucilvio.Solo.Webills.UseCases.Common;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewExpense;

namespace Lucilvio.Solo.Webills.UseCases.AddNewExpense
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
            if (command == null)
                throw new CommandNotInformed();

            var user = await this._dataStorage.GetUserById(command.UserId);

            if (user.NotDefined())
                throw new UserNotFound();

            user.AddExpense(command.Name, command.Category, command.Date, command.Value);

            await this._dataStorage.Persist(user);
        }
    }
}