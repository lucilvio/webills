using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions.RemoveExpense;

namespace Lucilvio.Solo.Webills.Transactions.RemoveIncome
{
    internal class RemoveIncomeComponent
    {
        private readonly IRemoveIncomeDataAccess _dataAccess;

        public RemoveIncomeComponent(IRemoveIncomeDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task Execute(RemoveIncomeInput input, Func<RemovedIncome, Task> onRemoveIncome = null)
        {
            var foundUser = await _dataAccess.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.RemoveIncome(input.Id);

            await _dataAccess.Persist(input.Id);

            if(onRemoveIncome != null)
                onRemoveIncome.Invoke(new RemovedIncome(foundUser.Id, input.Id));
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}