using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.FinancialControl.GetIncomesByFilter
{
    public record GetIncomesByFilterInput(Guid UserId) : Message<FoundIncomesByFilter>;

    internal class GetIncomesByFilterComponent
    {
        private readonly IDbConnection _connection;

        public GetIncomesByFilterComponent(IDbConnection connection)
        {
            this._connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public async Task Execute(GetIncomesByFilterInput message)
        {
            var query = "select Id, Name, Date, Value from financialControl.Incomes where UserId = @userId";

            var foundIncomes = await this._connection.QueryAsync<FoundIncomesByFilter.FilteredIncome>(query, new { message.UserId });
            message.SetResponse(new FoundIncomesByFilter(foundIncomes));
        }
    }
}