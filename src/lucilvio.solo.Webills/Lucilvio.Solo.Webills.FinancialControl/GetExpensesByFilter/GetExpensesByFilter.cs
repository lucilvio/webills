using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.FinancialControl.GetExpensesByFilter
{
    public record GetExpensesByFilterMessage(Guid UserId) : Message<FoundExpenses> { }

    internal class GetExpensesByFilter : IHandler<GetExpensesByFilterMessage>
    {
        private readonly GetExpensesByFilterDataAccess _dataAccess;

        public GetExpensesByFilter(GetExpensesByFilterDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task Execute(GetExpensesByFilterMessage message)
        {
            var foundExpenses = await this._dataAccess.GetExpensesByFilter(message.UserId);
            message.SetResponse(foundExpenses);
        }
    }
}