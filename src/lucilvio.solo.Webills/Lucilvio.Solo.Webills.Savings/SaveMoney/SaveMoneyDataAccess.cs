using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Savings.Domain;
using Lucilvio.Solo.Webills.Savings.Infraestructure.DataAccess;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Savings.SaveMoney
{
    internal class SaveMoneyDataAccess : ISaveMoneyDataAccess
    {
        private readonly SavingsContext _context;

        public SaveMoneyDataAccess(SavingsContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<User> GetUserById(Guid userId)
        {
            return this._context.Users.Include(u => u.SavingsAccounts).FirstOrDefaultAsync(u => u.Id == userId);
        }

        public Task Persist()
        {
            return this._context.SaveChangesAsync();
        }
    }
}