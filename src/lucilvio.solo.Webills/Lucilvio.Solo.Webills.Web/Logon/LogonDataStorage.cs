using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Lucilvio.Solo.Webills.UseCases.Logon;
using Lucilvio.Solo.Webills.Domain.Security.User;

namespace Lucilvio.Solo.Webills.Web.Logon
{
    public class LogonDataStorage : ILogonDataStorage
    {
        private readonly WebillsSecurityContext _context;

        public LogonDataStorage(WebillsSecurityContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUserByLogin(string login)
        {
            return await this._context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == login).ConfigureAwait(false);
        }
    }
}
