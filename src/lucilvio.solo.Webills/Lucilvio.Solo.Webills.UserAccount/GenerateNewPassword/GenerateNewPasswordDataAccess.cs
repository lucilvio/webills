using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lucilvio.Solo.Webills.UserAccount.Domain;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword
{
    internal class GenerateNewPasswordDataAccess : IGenerateNewPasswordDataAccess
    {
        private readonly DataContext _context;

        public GenerateNewPasswordDataAccess(DataContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmail(Email email)
        {
            return await _context.Users
                .Include(u => u.Account)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task Persist()
        {
            await _context.SaveChangesAsync();
        }
    }
}