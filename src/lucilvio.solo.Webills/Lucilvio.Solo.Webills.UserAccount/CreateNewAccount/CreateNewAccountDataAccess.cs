using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Domain;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.UserAccount.CreateNewAccount
{
    internal interface ICreateNewAccountDataAccess
    {
        Task<User> GetUserByLogin(Domain.Login login);
        Task Persist(User user);
    }

    internal class CreateNewAccountDataAccess : ICreateNewAccountDataAccess
    {
        private readonly UserAccountDataContext _context;

        public CreateNewAccountDataAccess(UserAccountDataContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUserByLogin(Domain.Login login)
        {
            return await this._context.Users
                .AsNoTracking()
                .Include(u => u.Account)
                .FirstOrDefaultAsync(u => u.Account.Login == login);
        }

        public async Task Persist(User user)
        {
            await this._context.Users.AddAsync(user);
            await this._context.SaveChangesAsync();
        }
    }
}