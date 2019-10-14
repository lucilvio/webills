using Lucilvio.Solo.Webills.Domain.User;
using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UseCases.EditIncome;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Web.Home.EditIncome
{
    public class EditIncomeDataStorageWithEf : IEditIncomeDataStorage
    {
        private readonly WebillsContext _context;

        public EditIncomeDataStorageWithEf(WebillsContext context)
        {
            this._context = context;
        }

        public User GetUser()
        {
            return this._context.Users.Include(u => u.Incomes).FirstOrDefault();
        }

        public async Task Persist(Guid incomeNumber, User foundUser)
        {
            await this._context.SaveChangesAsync();
        }
    }
}
