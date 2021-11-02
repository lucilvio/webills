using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.FinancialControl.GetUserTransactionsByFilter
{
    public record GetUserTransactionsByFilterMessage(Guid UserId) : Message<FoundTransactionsByFilter>;

    internal class GetUserTransactionsByFilter : IHandler<GetUserTransactionsByFilterMessage>
    {
        private readonly IDbConnection _dbConnection;

        public GetUserTransactionsByFilter(IDbConnection dbConnection)
        {
            this._dbConnection = dbConnection;
        }

        public async Task Execute(GetUserTransactionsByFilterMessage message)
        {
            var query = @"select Id, RecurrentExpenseId as RecurrencyId, Name, Date, UserId, Value, 'Expense' Type from financialControl.Expenses
                where userId = @userId
                UNION select Id, RecurrentIncomeId as RecurrencyId, Name, Date, UserId, Value, 'Income' Type from financialControl.Incomes
                where userId = @userId
                order by Date asc";

            var transactions = await this._dbConnection.QueryAsync<FoundTransactionsByFilter.FilteredTransaction>(query, new { message.UserId });

            message.SetResponse(new FoundTransactionsByFilter(transactions));
        }
    }
}