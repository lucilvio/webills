using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lucilvio.Solo.Webills.UserAccount.Domain;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.UserAccount.ForgotYourPassword
{
    internal class ForgotYourPasswordDataAccess : IForgotYourPasswordDataAccess
    {
        private readonly DataContext _context;

        public ForgotYourPasswordDataAccess(DataContext context)
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