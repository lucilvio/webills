using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    internal class RemoveExpenseComponent
    {
        private readonly IRemoveExpenseDataAccess _dataAccess;
        private readonly IBusSender _bus;

        public RemoveExpenseComponent(IRemoveExpenseDataAccess dataAccess, IBusSender bus)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            this._bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task Execute(RemoveExpenseInput input)
        {
            var foundUser = await this._dataAccess.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.RemoveExpense(input.Id);

            await this._dataAccess.Persist(input.Id);

            this._bus.SendEvent(new OnRemovedExpenseInput(foundUser.Id, input.Id));
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}