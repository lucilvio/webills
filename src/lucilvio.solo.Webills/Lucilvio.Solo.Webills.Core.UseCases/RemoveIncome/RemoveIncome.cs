using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.RemoveIncome;
using Lucilvio.Solo.Webills.Shared.Domain;
using Lucilvio.Solo.Webills.Shared.UseCases.Errors;

namespace Lucilvio.Solo.Webills.Core.UseCases.RemoveIncome
{
    public class RemoveIncome : IRemoveIncome
    {
        private readonly IRemoveIncomeDataStorage _dataStorage;

        public RemoveIncome(IRemoveIncomeDataStorage dataStorage)
        {
            this._dataStorage = dataStorage;
        }

        public async Task Execute(RemoveIncomeCommand command)
        {
            if (command.NotDefined())
                throw new CommandNotInformed();

            var foundUser = await this._dataStorage.GetUser();

            if (foundUser.NotDefined())
                throw new UserNotFound();

            foundUser.RemoveIncome(command.IncomeNumber);

            await this._dataStorage.Persist();
        }
    }
}