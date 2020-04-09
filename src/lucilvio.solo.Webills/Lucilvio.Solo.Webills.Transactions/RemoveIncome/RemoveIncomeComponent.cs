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

        public async Task Execute(IRemoveIncomeInput input)
        {
            var foundUser = await _dataAccess.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.RemoveIncome(input.IncomeId);

            await _dataAccess.Persist(input.IncomeId);
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}