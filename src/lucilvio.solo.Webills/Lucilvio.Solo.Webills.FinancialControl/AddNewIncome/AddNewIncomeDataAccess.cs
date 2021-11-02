using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Lucilvio.Solo.Webills.FinancialControl.Domain;
using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.FinancialControl.AddNewIncome
{
    internal class AddNewIncomeDataAccess
    {
        private readonly FinancialControlDataContext _context;
        private readonly IDbConnection _connection;

        public AddNewIncomeDataAccess(FinancialControlDataContext context,
            IDbConnection connection)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public async Task AddNewIncome(Income income)
        {
            await this._context.Incomes.AddAsync(income);
            await this._context.SaveChangesAsync();
        }

        public async Task AddNewRecurrentIncome(RecurrentIncome recurrentIncome)
        {
            var command = "insert into FinancialControl.RecurrentIncomes values(@id, @until, @frequency);";

            recurrentIncome.Incomes.ToList()
                .ForEach(e => command += $"insert into FinancialControl.Incomes values('{e.Id}', '{e.UserId}', '{e.Name}', '{e.Date}', {(int)e.Category}, {e.Value.Value}, @id);");

            await this._connection.ExecuteAsync(command, new
            {
                id = recurrentIncome.Id,
                until = recurrentIncome.Recurrency.Until,
                frequency = recurrentIncome.Recurrency.Frequency.Value
            });
        }
    }
}