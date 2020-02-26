using System.Threading.Tasks;

using Dapper;

using Lucilvio.Solo.Webills.Dashboard.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Dashboard.AddExpense
{
    internal class AddExpenseComponent : IComponent
    {
        private readonly DashBoardContext _context;

        public AddExpenseComponent(DashBoardContext context)
        {
            this._context = context;
        }

        public async Task Execute(IAddExpenseInput input)
        {
            using (var connection = this._context.Connection)
            {
                var sql = "insert into dashboard.Transactions values(@userId, @transactionId, @name, @date, @value, @category, @categoryName, 0, 1)";

                await connection.ExecuteAsync(sql, new
                {
                    userId = input.UserId,
                    transactionId = input.TransactionId,
                    name = input.Name,
                    date = input.Date,
                    value = input.Value,
                    category = input.Category,
                    categoryName = input.CategoryName
                });
            }
        }
    }
}
