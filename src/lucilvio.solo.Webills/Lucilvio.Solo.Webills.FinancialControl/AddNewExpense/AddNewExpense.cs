using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewExpense
{
    public record AddNewExpenseMessage(Guid UserId, string Name, string Category, 
        DateTime Date, decimal Value) : Message;
    
    internal class AddNewExpense : IHandler<AddNewExpenseMessage>
    {
        private readonly AddNewExpenseDataAccess _dataAccess;

        public AddNewExpense(AddNewExpenseDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task Execute(AddNewExpenseMessage message)
        {
            var newExpense = new Expense(message.UserId, message.Name, message.Category, message.Date, new TransactionValue(message.Value));
            await this._dataAccess.AddNewExpense(newExpense);
        }

        internal class Error
        {
            public class UserNotFound : Exception { }
        }
    }
}