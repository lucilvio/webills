using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.Domain;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    internal class GenerateNewPasswordDataAccess : IGenerateNewPasswordDataAccess
    {
        private readonly UserAccountContext _context;

        public GenerateNewPasswordDataAccess(UserAccountContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users
                .Include(u => u.Account)
                .FirstOrDefaultAsync(u => u.Email.Value == email);
        }

        public async Task Persist()
        {
            await _context.SaveChangesAsync();
        }
    }
}