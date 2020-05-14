using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions.EditExpense
{
    internal class EditExpenseComponent
    {
        private readonly IEditExpenseDataAccess _dataStorage;
        private readonly IBusSender _bus;

        public EditExpenseComponent(IEditExpenseDataAccess dataStorage, IBusSender bus)
        {
            this._dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
            this._bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task Execute(EditExpenseInput input)
        {
            var foundUser = await this._dataStorage.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            var editedExpense = foundUser.EditExpense(input.Id, input.Name, input.Category,
                input.Date, input.Value);

            await this._dataStorage.Persist();

            this._bus.SendEvent(new OnEditedExpenseInput(foundUser, editedExpense));
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}