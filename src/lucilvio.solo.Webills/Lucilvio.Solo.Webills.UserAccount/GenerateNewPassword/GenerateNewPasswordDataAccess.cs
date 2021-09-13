using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Domain;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    internal interface IGenerateNewPasswordDataAccess
    {
        Task<User> GetUserByEmail(Email email);
        Task Persist();
    }

    internal class GenerateNewPasswordDataAccess : IGenerateNewPasswordDataAccess
    {
        private readonly UserAccountDataContext _context;

        public GenerateNewPasswordDataAccess(UserAccountDataContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUserByEmail(Email email)
        {
            return await this._context.Users
                .Include(u => u.Account)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task Persist()
        {
            await this._context.SaveChangesAsync();
        }
    }
}