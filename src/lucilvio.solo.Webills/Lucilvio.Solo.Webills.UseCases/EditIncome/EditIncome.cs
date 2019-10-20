using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.Common;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditIncome;

namespace Lucilvio.Solo.Webills.UseCases.EditIncome
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
            if (command == null)
                throw new CommandNotInformed();

            var foundUser = await this._dataStorage.GetUser();

            if (foundUser == null)
                throw new UserNotFound();

            foundUser.AlterIncome(command.Number, command.Name, command.Date, command.Value);

            await this._dataStorage.Persist(command.Number, foundUser);
        }
    }
}
