using System.Threading.Tasks;

using Dapper;

using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Transactions.GetUserDashboardInfo
{
    internal class GetDashboardInfoByFilterComponent
    {
        private readonly TransactionsReadContext _context;

        public GetDashboardInfoByFilterComponent(TransactionsReadContext context)
        {
            this._context = context;
        }

        public async Task<GetDashboardInfoByFilterOutput> Execute(GetDashboardInfoByFilterInput input)
        {
            using (var con = this._context.Connection)
            {
                var query = @"
                    select	TotalSpent.qtd as TotalSpent,
	                TotalEarns.qtd as TotalEarns,
	                (TotalEarns.qtd - TotalSpent.qtd) as Balance
                    from
                    (select case when sum(val ue) is NULL then 0 else sum(value) end qtd from transactions.Expenses where userId = @userId) TotalSpent,
	                (select case when sum(value) is NULL then 0 else sum(value) end qtd from transactions.Incomes where userId = @userId) TotalEarns";

                return await con.QueryFirstOrDefaultAsync<GetDashboardInfoByFilterOutput>(query, new { userId = input.UserId });
            }
        }
    }
}
