﻿using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.CreateAccount;
using Lucilvio.Solo.Webills.UserAccount.Domain;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.UserAccount.CreateNewAccount
{
    internal class CreateNewAccountDataAccess : ICreateNewAccountDataAccess
    {
        private readonly DataContext _context;

        public CreateNewAccountDataAccess(DataContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByLogin(Domain.Login login)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Account)
                .FirstOrDefaultAsync(u => u.Account.Login == login);
        }

        public async Task Persist(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}