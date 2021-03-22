using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewRecurrentIncome
{
    public record AddNewRecurrentIncomeMessage(Guid UserId, string Name, string Category, DateTime Date, decimal Value, 
        DateTime Until, int Frequency);

    internal class AddNewRecurrentIncomeMessageHandler : IMessageHandler<AddNewRecurrentIncomeMessage>
    {
        private readonly AddNewRecurrentIncomeDataAccess _dataAccess;

        public AddNewRecurrentIncomeMessageHandler(AddNewRecurrentIncomeDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public async Task<dynamic> Execute(AddNewRecurrentIncomeMessage message)
        {
            var recurrentIncome = new RecurrentIncome(message.UserId, message.Name, message.Category,
                message.Date, new TransactionValue(message.Value), message.Until, message.Frequency);

            await _dataAccess.AddNewRecurrentIncome(recurrentIncome);

            return recurrentIncome;
        }
    }
}