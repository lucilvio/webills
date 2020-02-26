using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.EditExpense
{
    internal class EditExpenseComponent : IComponent
    {
        private readonly IEditExpenseDataAccess _dataStorage;

        public EditExpenseComponent(IEditExpenseDataAccess dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public async Task Execute(IEditExpenseInput input)
        {
            var foundUser = await _dataStorage.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.EditExpense(input.Id, input.Name, Enum.Parse<Category>(input.Category, true), input.Date,
                new TransactionValue(input.Value));

            await _dataStorage.Persist();
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}