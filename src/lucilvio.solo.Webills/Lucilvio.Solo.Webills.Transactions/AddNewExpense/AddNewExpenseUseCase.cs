using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.AddNewExpense
{
    internal class AddNewExpenseUseCase : IUseCase<AddNewExpenseCommand>
    {
        private readonly IAddNewExpenseDataAccess _dataAccess;

        public AddNewExpenseUseCase(IAddNewExpenseDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new System.ArgumentNullException(nameof(dataAccess));
        }

        public async Task Execute(AddNewExpenseCommand command)
        {
            var foundUser = await this._dataAccess.GetUserById(command.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.AddExpense(command.Name, Enum.Parse<Category>(command.Category, true), command.Date, new TransactionValue(command.Value));

            await this._dataAccess.Persist();
        }

        internal class Error
        {
            public class UserNotFound : Exception { }
        }
    }
}
