using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.FinancialControl.GetIncomeById
{
    public record GetIncomeByIdMessage(Guid UserId, Guid Id) : Message<FoundIncomeById>;

    internal class GetIncomeById : IMessageHandler<GetIncomeByIdMessage>
    {
        private readonly IDbConnection _connection;

        public GetIncomeById(IDbConnection connection)
        {
            this._connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public async Task Execute(GetIncomeByIdMessage message)
        {
            var queyr = "select Id, Name, Date, Value from Transactions.Incomes where Id = @id";
            var foundIndome = await this._connection.QueryFirstOrDefaultAsync<FoundIncomeById>(queyr, new { id = message.Id });

            message.SetResponse(foundIndome);
        }
    }
}
