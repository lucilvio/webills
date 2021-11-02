using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.FinancialControl.GetExpenseById
{
    public record GetExpenseByIdMessage(Guid UserId, Guid Id) : Message<FoundExpenseById>;

    internal class GetExpenseByIdComponent
    {
        private readonly IDbConnection _connection;

        public GetExpenseByIdComponent(IDbConnection connection)
        {
            this._connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public async Task Execute(GetExpenseByIdMessage message)
        {
            var query = @"select Id, Name, Category, Date, Value from Transactions.Expenses
                where Id = @id";

            message.SetResponse(await this._connection.QueryFirstOrDefaultAsync<FoundExpenseById>(query, new { id = message.Id }));
        }
    }
}