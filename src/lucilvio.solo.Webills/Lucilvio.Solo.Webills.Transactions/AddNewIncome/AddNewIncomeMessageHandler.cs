using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.AddNewIncome
{
    internal class AddNewIncomeMessageHandler
    {
        private readonly IAddNewIncomeDataAccess _dataStorage;
        private readonly IEventBus _bus;

        public AddNewIncomeMessageHandler(IAddNewIncomeDataAccess dataStorage, IEventBus bus)
        {
            this._dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
            this._bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task Execute(IAddNewIncomeMessage input)
        {
            var foundUser = await this._dataStorage.GetUserById(input.UserId);

            if (foundUser == null)
                throw new Error.UserNotFound();

            if(!Enum.TryParse(typeof(Income.IncomeCategory), input.Category, out var category))
                category = Income.IncomeCategory.Other;

            var createdIncome = foundUser.AddIncome(input.Name, input.Date, (Income.IncomeCategory)category, new TransactionValue(input.Value));

            await this._dataStorage.Persist();

            this._bus.Publish("NewIncomeAdded", new OnAddIncomeInput(foundUser, createdIncome));
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}