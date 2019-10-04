using Lucilvio.Solo.Webills.Tests;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class AddNewIncome : IAddNewIncome
    {
        private readonly IAddNewIncomeDataStorage _dataStorage;

        public AddNewIncome(IAddNewIncomeDataStorage dataStorage)
        {
            this._dataStorage = dataStorage;
        }

        public void Execute(AddNewIncomeCommand command)
        {
            var user = new User();
            user.AddIncome(new Income(command.Name, command.Date, command.Value));

            this._dataStorage.AddUserIncomeData(user);
        }
    }
}