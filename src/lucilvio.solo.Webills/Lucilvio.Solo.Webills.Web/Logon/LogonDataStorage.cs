using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.Logon;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Web.Logon
{
    public class LogonDataStorage : ILogonDataStorage
    {
        private readonly WebillsContext _context;

        public LogonDataStorage(WebillsContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUserByLogin(Login login)
        {
            return await this._context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == login).ConfigureAwait(false);
        }
    }
}
