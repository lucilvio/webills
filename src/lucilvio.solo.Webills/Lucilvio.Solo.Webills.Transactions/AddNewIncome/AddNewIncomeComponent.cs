using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.AddNewIncome
{
    internal class AddNewIncomeComponent
    {
        private readonly IAddNewIncomeDataAccess _dataStorage;
        private readonly IBusSender _bus;

        public AddNewIncomeComponent(IAddNewIncomeDataAccess dataStorage, IBusSender bus)
        {
            this._dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
            this._bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task Execute(AddNewIncomeInput input)
        {
            var foundUser = await this._dataStorage.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            var createdIncome = foundUser.AddIncome(input.Name, input.Date, new TransactionValue(input.Value));

            await this._dataStorage.Persist();

            this._bus.SendEvent(new OnAddIncomeInput(foundUser, createdIncome));
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}