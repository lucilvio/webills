using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewRecurrentIncome
{
    internal class AddNewRecurrentIncomeDataAccess
    {
        private readonly IDbConnection _dbConnection;

        public AddNewRecurrentIncomeDataAccess(IDbConnection dbConnection)
        {
            this._dbConnection = dbConnection;
        }

        public async Task AddNewRecurrentIncome(RecurrentIncome recurrentIncome)
        {
            var command = "insert into FinancialControl.RecurrentIncomes values(@id, @until, @frequency);";

            recurrentIncome.Incomes.ToList()
                .ForEach(e => command += $"insert into FinancialControl.Incomes values('{e.Id}', '{e.UserId}', '{e.Name}', '{e.Date}', {(int)e.Category}, {e.Value.Value}, @id);");

            this._dbConnection.Open();
            using var transaction = _dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            
            try
            {
                await this._dbConnection.ExecuteAsync(command, new
                {
                    id = recurrentIncome.Id,
                    until = recurrentIncome.Recurrency.Until,
                    frequency = recurrentIncome.Recurrency.Frequency.Value
                }, transaction);

                transaction.Commit();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}