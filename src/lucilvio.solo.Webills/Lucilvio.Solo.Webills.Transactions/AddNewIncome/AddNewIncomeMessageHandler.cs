using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewIncome
{
    public record AddNewIncomeMessage(Guid UserId, string Name, string Category, DateTime Date, decimal Value);
    
    internal class AddNewIncomeMessageHandler : IMessageHandler<AddNewIncomeMessage>
    {
        private readonly IAddNewIncomeDataAccess _dataStorage;

        public AddNewIncomeMessageHandler(IAddNewIncomeDataAccess dataStorage)
        {
            this._dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
        }

        public async Task<dynamic> Execute(AddNewIncomeMessage message)
        {
            var newIncome = new Income(message.UserId, message.Name, message.Category, message.Date, new TransactionValue(message.Value));
            await this._dataStorage.AddNewIncome(newIncome);

            return newIncome;
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}