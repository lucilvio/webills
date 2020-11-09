using Dapper;
using System.Data;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions.GetUserDashboardInfo;

namespace Lucilvio.Solo.Webills.Transactions.GetUserFinancialInformation
{
    internal class GetUserFinancialInformationMessageHandler
    {
        private readonly IDbConnection _connection;

        public GetUserFinancialInformationMessageHandler(IDbConnection connection)
        {
            this._connection = connection;
        }

        public async Task<UserFinancialInformation> Execute(IGetUserFinancialInformationMessage input)
        {
            var query = @"
                select	TotalSpent.qtd as Expenses,
	            TotalEarns.qtd as Earns,
	            (TotalEarns.qtd - TotalSpent.qtd) as Balance
                from
                (select case when sum(value) is NULL then 0 else sum(value) end qtd from transactions.Expenses where userId = @userId) TotalSpent,
	            (select case when sum(value) is NULL then 0 else sum(value) end qtd from transactions.Incomes where userId = @userId) TotalEarns";

            return await this._connection.QueryFirstOrDefaultAsync<UserFinancialInformation>(query, new { userId = input.UserId });
        }
    }
}