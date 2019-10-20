using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UseCases.Common;
using Lucilvio.Solo.Webills.UseCases.Contracts.RemoveIncome;

namespace Lucilvio.Solo.Webills.UseCases.RemoveIncome
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
            if (command == null)
                throw new CommandNotInformed();

            var foundUser = await this._dataStorage.GetUser();

            if (foundUser == null)
                throw new UserNotFound();

            foundUser.RemoveIncome(command.IncomeNumber);

            await this._dataStorage.Persist();
        }
    }
}