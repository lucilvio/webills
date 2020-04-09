using System;
using System.Threading.Tasks;
using Dapper;
using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Transactions.GetIncomesByFilter
{
    internal class GetIncomesByFilterComponent
    {
        private readonly TransactionsReadContext _context;

        public GetIncomesByFilterComponent(TransactionsReadContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetIncomesByFilterOutput> Execute(GetIncomesByFilterInput input)
        {
            using (var connection = this._context.Connection)
            {
                var sql = "select Id, Name, Category, Date, Value from Transactions.Expenses where UserId = @userId";

                var result = await connection.QueryAsync<GetIncomesByFilterOutput.Income>(sql, new { input.UserId });
                return new GetIncomesByFilterOutput(result);
            }
        }
    }
}
