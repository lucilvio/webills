using System.Threading.Tasks;

using Dapper;

using Lucilvio.Solo.Webills.Dashboard.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Dashboard.AddExpense
{
    internal class AddTransactionComponent
    {
        private readonly DashBoardContext _context;

        public AddTransactionComponent(DashBoardContext context)
        {
            this._context = context;
        }

        public async Task Execute(AddTransactionInput input)
        {
            using (var connection = this._context.Connection)
            {
                var sql = @"insert into dashboard.Transactions values(@userId, @id, @name, @date, @value, 
                    @category, @isIncome, @isExpense)";

                await connection.ExecuteAsync(sql, new
                {
                    id = input.Id,
                    userId = input.UserId,
                    name = input.Name,
                    date = input.Date,
                    value = input.Value,
                    category = input.Category,
                    isIncome = input.IsIncome,
                    isExpense = input.IsExpense
                });
            }
        }
    }
}