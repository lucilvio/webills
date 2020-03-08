using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    internal interface ISendNewPasswordDataAccess
    {
        Task<User> GetUserByLogin(Domain.Login login);
        Task Persist();
    }

    internal class SendNewPasswordDataAccess : ISendNewPasswordDataAccess
    {
        private readonly UserAccountContext _context;

        public SendNewPasswordDataAccess(UserAccountContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByLogin(Domain.Login login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task Persist()
        {
            await _context.SaveChangesAsync();
        }
    }
}