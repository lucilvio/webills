using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.EditIncome
{
    internal class EditIncomeComponent
    {
        private readonly IEditIncomeDataAccess _dataStorage;
        private readonly IBusSender _bus;

        public EditIncomeComponent(IEditIncomeDataAccess dataStorage, IBusSender bus)
        {
            _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
            this._bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task Execute(EditIncomeInput input)
        {
            var foundUser = await _dataStorage.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            var editedIncome = foundUser.EditIncome(input.Id, input.Name, input.Date, new TransactionValue(input.Value));

            await _dataStorage.Persist();

            this._bus.SendEvent(new OnEditIncomeInput(foundUser, editedIncome));
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}