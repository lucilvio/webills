using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    internal class CreateUserAccountDataAccess : ICreateUserAccountDataAccess
    {
        private readonly UserAccountContext _context;

        public CreateUserAccountDataAccess(UserAccountContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAccountByLogin(Domain.Login login)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task Persist(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}