using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.FinancialControl.GetUserDashboardInfo;

namespace Lucilvio.Solo.Webills.FinancialControl.GetUserFinancialInformation
{
    public record GetUserFinancialInformationMessage(Guid UserId) : Message<UserFinancialInformation>;

    internal class GetUserFinancialInformation : IHandler<GetUserFinancialInformationMessage>
    {
        private readonly IDbConnection _connection;

        public GetUserFinancialInformation(IDbConnection connection)
        {
            this._connection = connection;
        }

        public async Task Execute(GetUserFinancialInformationMessage message)
        {
            var query = @"
                select TotalSpent.qtd as Expenses,
	            TotalEarns.qtd as Earns	            
                from
                (select case when sum(value) is NULL then 0 else sum(value) end qtd from financialControl.Expenses where userId = @userId) TotalSpent,
	            (select case when sum(value) is NULL then 0 else sum(value) end qtd from financialControl.Incomes where userId = @userId) TotalEarns";

            message.SetResponse(await this._connection.QueryFirstOrDefaultAsync<UserFinancialInformation>(query, new { userId = message.UserId }));
        }
    }
}