using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.EditIncome
{
    internal class EditIncomeComponent
    {
        private readonly IEditIncomeDataAccess _dataStorage;

        public EditIncomeComponent(IEditIncomeDataAccess dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public async Task Execute(EditIncomeInput input, Func<EditedIncome, Task> onEditIncome)
        {
            var foundUser = await _dataStorage.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            var editedIncome = foundUser.EditIncome(input.Id, input.Name, input.Date, new TransactionValue(input.Value));

            await _dataStorage.Persist();

            if(onEditIncome != null)
                onEditIncome.Invoke(new EditedIncome(foundUser, editedIncome));
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}