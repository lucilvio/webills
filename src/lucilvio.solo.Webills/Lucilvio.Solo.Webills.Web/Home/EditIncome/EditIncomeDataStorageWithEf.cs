using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Lucilvio.Solo.Webills.Core.UseCases.EditIncome;
using Lucilvio.Solo.Webills.Core.Domain.User;

namespace Lucilvio.Solo.Webills.Web.Home.EditIncome
{
    public class EditIncomeDataStorageWithEf : IEditIncomeDataStorage
    {
        private readonly WebillsCoreContext _context;

        public EditIncomeDataStorageWithEf(WebillsCoreContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUser()
        {
            return await this._context.Users.Include(u => u.Incomes).FirstOrDefaultAsync();
        }

        public async Task Persist(Guid incomeNumber, User foundUser)
        {
            var income = this._context.Set<Income>().FirstOrDefault(i => i.Id == incomeNumber);
            this._context.Entry<Income>(income).State = EntityState.Deleted;

            await this._context.SaveChangesAsync();
        }
    }
}
