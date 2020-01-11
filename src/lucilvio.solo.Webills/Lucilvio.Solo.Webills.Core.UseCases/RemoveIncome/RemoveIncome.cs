using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Domain.Shared;
using Lucilvio.Solo.Webills.UseCases.Shared.Errors;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.RemoveIncome;

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