using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    internal class LoginDataAccess : ILoginDataAccess
    {
        private readonly UserAccountContext _context;

        public LoginDataAccess(UserAccountContext context)
        {
            this._context = context;
        }

        public Task<User> GetUserByLogin(Domain.Login login)
        {
            return this._context.Users
                .Include(u => u.Account)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Account.Login == login);
        }
    }
}