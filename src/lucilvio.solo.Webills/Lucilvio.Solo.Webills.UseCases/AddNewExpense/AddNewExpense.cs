using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;
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

            var foundUser = await this._dataStorage.GetUser();

            if (foundUser == null)
                throw new UserNotFound();

            foundUser.AddExpense(command.Name, command.Date, command.Value);

            await this._dataStorage.Persist(foundUser);
        }
    }
}