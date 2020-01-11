using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Domain.Shared;
using Lucilvio.Solo.Webills.UseCases.Shared.Errors;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.EditIncome;

namespace Lucilvio.Solo.Webills.Core.UseCases.EditIncome
{
    public class EditIncome : IEditIncome
    {
        private readonly IEditIncomeDataStorage _dataStorage;

        public EditIncome(IEditIncomeDataStorage dataStorage)
        {
            this._dataStorage = dataStorage;
        }

        public async Task Execute(EditIncomeCommand command)
        {
            if (command.NotDefined())
                throw new CommandNotInformed();

            var foundUser = await this._dataStorage.GetUser();

            if (foundUser.NotDefined())
                throw new UserNotFound();

            foundUser.AlterIncome(command.Id, command.Name, command.Date, command.Value);

            await this._dataStorage.Persist(command.Id, foundUser);
        }
    }
}
