using Dapper;
using Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage;
using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
{
    public class GetUserIncomesByFilterQueryHandler : IGetUserIncomesByFilterQueryHandler
    {
        private readonly WebillsReadContext _context;

        public GetUserIncomesByFilterQueryHandler(WebillsReadContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetUserIncomesByFilterQueryResult> Execute(GetUserIncomesByFilterQuery getIncomesByFilterQuery)
        {
            var query = @$"select   i.Id,
		                            i.Name,
                                    i.Date,
                                    i.Value
                           from	    Transactions.Incomes i
                           where	i.UserId = @userId;";

            using (var con = this._context.GetConnection())
            {
                var incomes = await con.QueryAsync<UserIncomeData>(query, new { userId = getIncomesByFilterQuery.UserId }).ConfigureAwait(false);

                return new GetUserIncomesByFilterQueryResult(incomes);
            }
        }
    }
}