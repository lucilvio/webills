using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Lucilvio.Solo.Webills.UseCases.Logon;
using Lucilvio.Solo.Webills.Security.Domain.User;

namespace Lucilvio.Solo.Webills.Infraestructure.EFDataStorage.Security
{
    public class LogonDataStorageWithEf : ILogonDataStorage
    {
        private readonly WebillsSecurityContext _context;

        public LogonDataStorageWithEf(WebillsSecurityContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUserByLogin(string login)
        {
            return await this._context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == login).ConfigureAwait(false);
        }
    }
}
