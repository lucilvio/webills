using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    internal class RemoveExpenseComponent
    {
        private readonly IRemoveExpenseDataAccess _dataAccess;

        public RemoveExpenseComponent(IRemoveExpenseDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public async Task<RemovedExpense> Execute(RemoveExpenseInput input)
        {
            var foundUser = await this._dataAccess.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.RemoveExpense(input.Id);

            await this._dataAccess.Persist(input.Id);

            return new RemovedExpense(foundUser.Id, input.Id);
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}