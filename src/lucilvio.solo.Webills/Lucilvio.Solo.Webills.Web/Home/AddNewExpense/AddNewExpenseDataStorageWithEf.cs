using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Core.UseCases.AddNewExpense;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Web
{
    public class AddNewExpenseDataStorageWithEf : IAddNewExpenseDataStorage
    {
        private readonly WebillsCoreContext _context;

        public AddNewExpenseDataStorageWithEf(WebillsCoreContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await this._context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Persist(User user)
        {
            await this._context.SaveChangesAsync();
        }
    }
}