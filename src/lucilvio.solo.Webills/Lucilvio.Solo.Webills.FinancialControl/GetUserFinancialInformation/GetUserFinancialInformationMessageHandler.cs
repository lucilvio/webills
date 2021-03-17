using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Lucilvio.Solo.Webills.FinancialControl.GetUserDashboardInfo;

namespace Lucilvio.Solo.Webills.FinancialControl.GetUserFinancialInformation
{
    public record GetUserFinancialInformationMessage(Guid UserId);

    internal class GetUserFinancialInformationMessageHandler : IMessageHandler<GetUserFinancialInformationMessage>
    {
        private readonly IDbConnection _connection;

        public GetUserFinancialInformationMessageHandler(IDbConnection connection)
        {
            this._connection = connection;
        }

        public async Task<dynamic> Execute(GetUserFinancialInformationMessage input)
        {
            var query = @"
                select TotalSpent.qtd as Expenses,
	            TotalEarns.qtd as Earns	            
                from
                (select case when sum(value) is NULL then 0 else sum(value) end qtd from financialControl.Expenses where userId = @userId) TotalSpent,
	            (select case when sum(value) is NULL then 0 else sum(value) end qtd from financialControl.Incomes where userId = @userId) TotalEarns";

            return await this._connection.QueryFirstOrDefaultAsync<UserFinancialInformation>(query, new { userId = input.UserId });
        }
    }
}