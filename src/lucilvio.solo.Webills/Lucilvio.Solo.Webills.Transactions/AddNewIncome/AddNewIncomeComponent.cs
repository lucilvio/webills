using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.AddNewIncome
{
    internal class AddNewIncomeComponent
    {
        private readonly IAddNewIncomeDataAccess _dataStorage;

        public AddNewIncomeComponent(IAddNewIncomeDataAccess dataStorage)
        {
            _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
        }

        public async Task<AddedIncome> Execute(AddNewIncomeInput input)
        {
            var foundUser = await this._dataStorage.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            var createdIncome = foundUser.AddIncome(input.Name, input.Date, new TransactionValue(input.Value));

            await this._dataStorage.Persist();

            return new AddedIncome(foundUser, createdIncome);
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}