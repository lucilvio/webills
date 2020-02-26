using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.EditIncome
{
    internal class EditIncomeComponent : IComponent
    {
        private readonly IEditIncomeDataAccess _dataStorage;

        public EditIncomeComponent(IEditIncomeDataAccess dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public async Task Execute(IEditIncomeInput input)
        {
            var foundUser = await _dataStorage.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.EditIncome(input.Id, input.Name, input.Date, new TransactionValue(input.Value));

            await _dataStorage.Persist();
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}