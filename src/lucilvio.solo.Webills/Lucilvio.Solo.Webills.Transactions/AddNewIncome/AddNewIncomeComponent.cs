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

        public async Task Execute(AddNewIncomeInput input)
        {
            var foundUser = await this._dataStorage.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.AddIncome(input.Name, input.Date, new TransactionValue(input.Value));

            await this._dataStorage.Persist();
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}