using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewExpense;

namespace Lucilvio.Solo.Webills.Web
{
    public class AddNewExpenseDataStorageWithEf : IAddNewExpenseDataStorage
    {
        private readonly WebillsContext _context;

        public AddNewExpenseDataStorageWithEf(WebillsContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUser()
        {
            return await this._context.Users.FirstOrDefaultAsync();
        }

        public async Task Persist(User user)
        {
            await this._context.SaveChangesAsync();
        }
    }
}