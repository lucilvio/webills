using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions.Domain;
using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Transactions.Infraestructure.SyncUser
{
    internal class SyncUserDataAccess : ISyncUserDataAccess
    {
        private readonly TransactionsContext _context;

        public SyncUserDataAccess(TransactionsContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<User> GetUserById(Guid id)
        {
            return this._context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task InsertUser(User user)
        {
            await this._context.Users.AddAsync(user);
            await this._context.SaveChangesAsync();
        }
    }
}