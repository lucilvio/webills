using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewIncome
{
    internal class AddNewIncome : IMessageHandler<AddNewIncomeMessage>
    {
        private readonly AddNewIncomeDataAccess _dataAccess;

        public AddNewIncome(AddNewIncomeDataAccess dataStorage)
        {
            this._dataAccess = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
        }

        public async Task Execute(AddNewIncomeMessage message)
        {
            if (message.IsRecurrent)
            {
                var newRecurrentIncome = new RecurrentIncome(message.UserId, message.Name, message.Category,
                    message.Date, new TransactionValue(message.Value), message.Recurrency.RepeatUntil, message.Recurrency.Frequency);

                await this._dataAccess.AddNewRecurrentIncome(newRecurrentIncome);
            }
            else
            {
                var newIncome = new Income(message.UserId, message.Name, message.Category, message.Date,
                    new TransactionValue(message.Value));

                await this._dataAccess.AddNewIncome(newIncome);
            }
        }

        internal class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}