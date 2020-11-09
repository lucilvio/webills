using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.EventBus;

namespace Lucilvio.Solo.Webills.Transactions.RemoveIncome
{
    internal class RemoveIncomeComponent
    {
        private readonly IRemoveIncomeDataAccess _dataAccess;
        private readonly IEventBus _bus;

        public RemoveIncomeComponent(IRemoveIncomeDataAccess dataAccess, IEventBus bus)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            this._bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task Execute(RemoveIncomeInput input)
        {
            var foundUser = await _dataAccess.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.RemoveIncome(input.Id);

            await _dataAccess.Persist(input.Id);
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}