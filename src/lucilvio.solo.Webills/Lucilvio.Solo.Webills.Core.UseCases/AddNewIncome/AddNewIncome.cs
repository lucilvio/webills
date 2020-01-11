using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.AddNewIncome;
using Lucilvio.Solo.Webills.Domain.Shared;
using Lucilvio.Solo.Webills.UseCases.Shared.Errors;

namespace Lucilvio.Solo.Webills.Core.UseCases.AddNewIncome
{
    public class AddNewIncome : IAddNewIncome
    {
        private readonly IAddNewIncomeDataStorage _dataStorage;

        public AddNewIncome(IAddNewIncomeDataStorage dataStorage)
        {
            this._dataStorage = dataStorage;
        }

        public async Task Execute(AddNewIncomeCommand command)
        {
            if (command.NotDefined())
                throw new CommandNotInformed();

            var foundUser = await this._dataStorage.GetUser();

            if (foundUser.NotDefined())
                throw new UserNotFound();

            foundUser.AddIncome(command.Name, command.Date, command.Value);

            await this._dataStorage.Persist(foundUser);
        }
    }
}