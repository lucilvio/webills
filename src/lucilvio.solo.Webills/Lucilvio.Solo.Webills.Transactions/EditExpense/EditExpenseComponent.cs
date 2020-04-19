using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions.EditExpense
{
    internal class EditExpenseComponent
    {
        private readonly IEditExpenseDataAccess _dataStorage;

        public EditExpenseComponent(IEditExpenseDataAccess dataStorage)
        {
            this._dataStorage = dataStorage;
        }

        public async Task<EditedExpense> Execute(EditExpenseInput input)
        {
            var foundUser = await this._dataStorage.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            var editedExpense = foundUser.EditExpense(input.Id, input.Name, input.Category,
                input.Date, input.Value);

            await this._dataStorage.Persist();

            return new EditedExpense(foundUser, editedExpense);
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}