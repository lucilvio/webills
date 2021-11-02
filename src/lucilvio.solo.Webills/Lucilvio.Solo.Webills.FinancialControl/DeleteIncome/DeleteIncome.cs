using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.FinancialControl.DeleteIncome
{
    public record DeleteIncomeMessage(Guid id) : Message;

    internal class DeleteIncome : IHandler<DeleteIncomeMessage>
    {
        private readonly DeleteIncomeDataAccess _dataAccess;

        public DeleteIncome(DeleteIncomeDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task Execute(DeleteIncomeMessage message)
        {
            var foundIncome = await this._dataAccess.GetIncomeById(message.id);

            if (foundIncome.IsRecurrent)
                await this._dataAccess.DeleteIncomeAndRecurrencies(foundIncome.RecurrentIncomeId);
            else
                await this._dataAccess.DeleteIncome(foundIncome.Id);
        }
    }
}