using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.FinancialControl.DeleteIncome
{
    public record DeleteIncomeMessage(Guid id);

    internal class DeleteIncomeMessageHandler : IMessageHandler<DeleteIncomeMessage>
    {
        private readonly DeleteIncomeDataAccess _dataAccess;

        public DeleteIncomeMessageHandler(DeleteIncomeDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<dynamic> Execute(DeleteIncomeMessage message)
        {
            var foundIncome = await this._dataAccess.GetIncomeById(message.id);

            if (foundIncome.IsRecurrent)
                await this._dataAccess.DeleteIncomeAndRecurrencies(foundIncome.RecurrentIncomeId);
            else
                await this._dataAccess.DeleteIncome(foundIncome.Id);

            return new { };
        }
    }
}