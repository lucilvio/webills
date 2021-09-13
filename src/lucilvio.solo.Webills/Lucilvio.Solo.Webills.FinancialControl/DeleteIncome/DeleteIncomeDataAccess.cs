using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Lucilvio.Solo.Webills.FinancialControl.Domain;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.FinancialControl.DeleteIncome
{
    internal class DeleteIncomeDataAccess
    {
        private readonly IDbConnection _dbConnection;
        private readonly FinancialControlDataContext _context;

        public DeleteIncomeDataAccess(FinancialControlDataContext context, IDbConnection dbConnection)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        internal async Task<Income> GetIncomeById(Guid id)
        {
            return await this._context.Incomes.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        internal async Task DeleteIncomeAndRecurrencies(Guid? recurrentIncomeId)
        {
            var command = "delete from FinancialControl.Incomes where RecurrentIncomeId = @recurrentIncomeId";

            this._dbConnection.Open();
            await this._dbConnection.ExecuteAsync(command, new { recurrentIncomeId });
        }

        internal async Task DeleteIncome(Guid id)
        {
            var command = "delete from FinancialControl.Incomes where id = @id";

            this._dbConnection.Open();
            await this._dbConnection.ExecuteAsync(command, new { id });
        }
    }
}