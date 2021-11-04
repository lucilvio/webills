using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewRecurrentExpense
{
    internal class AddNewRecurrentExpenseDataAccess
    {
        private readonly IDbConnection _dbConnection;

        public AddNewRecurrentExpenseDataAccess(IDbConnection dbConnection)
        {
            this._dbConnection = dbConnection;
        }

        public async Task AddNewRecurrentExpense(RecurrentExpense recurrentExpense)
        {
            var command = "insert into FinancialControl.RecurrentExpenses values(@id, @until, @frequency);";

            recurrentExpense.Expenses.ToList()
                .ForEach(e => command += $"insert into FinancialControl.Expenses values('{e.Id}', '{e.UserId}', '{e.Name}', '{e.Date}', {(int)e.Category}, {e.Value.Value}, @id);");

            await this._dbConnection.ExecuteAsync(command, new
            {
                id = recurrentExpense.Id,
                until = recurrentExpense.Recurrency.Until,
                frequency = recurrentExpense.Recurrency.Frequency.Value
            });
        }
    }
}