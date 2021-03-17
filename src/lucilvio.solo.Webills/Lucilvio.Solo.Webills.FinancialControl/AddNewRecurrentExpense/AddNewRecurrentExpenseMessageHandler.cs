using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewRecurrentExpense
{
    public record AddNewRecurrentExpenseMessage(Guid UserId, string Name, string Category, DateTime Date, decimal Value, DateTime Until,
        int frequency);

    internal class AddNewRecurrentExpenseMessageHandler : IMessageHandler<AddNewRecurrentExpenseMessage>
    {
        private readonly IAddNewRecurrentExpenseDataAccess _dataAccess;

        public AddNewRecurrentExpenseMessageHandler(IAddNewRecurrentExpenseDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public async Task<dynamic> Execute(AddNewRecurrentExpenseMessage message)
        {
            var newRecurrentExpense = new RecurrentExpense(message.UserId, message.Name, message.Category, message.Date,
                new TransactionValue(message.Value), message.Until, message.frequency);

            await this._dataAccess.AddNewRecurrentExpense(newRecurrentExpense);

            return newRecurrentExpense;
        }
    }
}