﻿using System;
using System.Linq;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;
using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Transactions.RemoveIncome
{
    internal class RemoveIncomeDataAccess : IRemoveIncomeDataAccess
    {
        private readonly TransactionsContext _context;

        public RemoveIncomeDataAccess(TransactionsContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<User> GetUserById(Guid id)
        {
            return this._context.Users.Include(u => u.Incomes).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Persist(Guid id)
        {
            this._context.Entry(this._context.Set<Income>().FirstOrDefault(e => e.Id == id)).State = EntityState.Deleted;

            await this._context.SaveChangesAsync();
        }
    }
}