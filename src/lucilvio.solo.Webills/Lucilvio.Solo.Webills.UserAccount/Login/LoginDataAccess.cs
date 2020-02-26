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

        public Task<User> GetUserByLogin(string login)
        {
            return this._context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == new Domain.Login(login));
        }
    }
}