using Dapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
{
    public class GetUserExpensesByFilterQueryHandler : IGetUserExpensesByFilterQueryHandler
    {
        private readonly WebillsReadContext _context;

        public GetUserExpensesByFilterQueryHandler(WebillsReadContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetUserExpensesByFilterQueryResult> Execute(GetUserExpensesByFilterQuery getExpensesByFilterQuery)
        {
            var query = @$"select   e.Id,
		                            e.Name,
                                    e.Date,
                                    e.Value
                           from	    Expenses e
                           where	e.UserId = @userId;";

            using (var con = this._context.GetConnection())
            {
                var expenses = await con.QueryAsync<UserExpenseData>(query, new { userId = getExpensesByFilterQuery.UserId }).ConfigureAwait(false);

                return new GetUserExpensesByFilterQueryResult(expenses);
            }
        }
    }
}