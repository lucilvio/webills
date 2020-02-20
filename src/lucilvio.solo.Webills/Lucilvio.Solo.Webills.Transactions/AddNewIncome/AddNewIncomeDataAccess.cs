using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;
using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Transactions.AddNewIncome
{
    internal class AddNewIncomeDataAccess : IAddNewIncomeDataAccess
    {
        private readonly TransactionsContext _context;

        public AddNewIncomeDataAccess(TransactionsContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        Task<User> IAddNewIncomeDataAccess.GetUserById(Guid id)
        {
            return this._context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        async Task IAddNewIncomeDataAccess.Persist()
        {
            await this._context.SaveChangesAsync();
        }
    }
}