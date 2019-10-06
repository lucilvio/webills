using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.AddNewIncome
{
    public class AddNewIncome : IAddNewIncome
    {
        private readonly IAddNewIncomeDataStorage _dataStorage;

        public AddNewIncome(IAddNewIncomeDataStorage dataStorage)
        {
            this._dataStorage = dataStorage;
        }

        public void Execute(AddNewIncomeCommand command)
        {
            if (command == null)
                throw new CommandNotInformed();

            var foundUser = this._dataStorage.GetUser();

            if (foundUser == null)
                throw new UserNotFound();

            foundUser.AddIncome(new Income(command.Name, command.Date, command.Value));

            this._dataStorage.Persist(foundUser);
        }
    }
}