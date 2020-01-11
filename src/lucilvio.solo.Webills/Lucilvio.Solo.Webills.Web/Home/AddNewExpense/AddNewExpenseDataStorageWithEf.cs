using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewExpense;
using System;

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