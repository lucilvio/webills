using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewExpense
{
    public record AddNewExpenseMessage(Guid UserId, string Name, string Category, DateTime Date, decimal Value);
    
    internal class AddNewExpenseMessageHandler : IMessageHandler<AddNewExpenseMessage>
    {
        private readonly IAddNewExpenseDataAccess _dataAccess;

        public AddNewExpenseMessageHandler(IAddNewExpenseDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<dynamic> Execute(AddNewExpenseMessage message)
        {
            var newExpense = new Expense(message.UserId, message.Name, message.Category, message.Date, new TransactionValue(message.Value));
            await this._dataAccess.AddNewExpense(newExpense);

            return newExpense;
        }

        internal class Error
        {
            public class UserNotFound : Exception { }
        }
    }
}