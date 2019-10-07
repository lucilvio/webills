using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;

namespace Lucilvio.Solo.Webills.Web
{
    public class AddNewIncomeDataStorageWithEf : IAddNewIncomeDataStorage
    {
        private readonly WebillsContext _context;

        public AddNewIncomeDataStorageWithEf(WebillsContext context)
        {
            this._context = context;
        }

        public User GetUser()
        {
            return this._context.Users.FirstOrDefault();
        }

        public async Task Persist(User user)
        {
            await this._context.SaveChangesAsync();
        }
    }
}