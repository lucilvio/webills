using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserProfile.Domain;
using Lucilvio.Solo.Webills.UserProfile.RegisterUser;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.UserProfile.DataStorage
{
    class RegisterUserDataStorage : IRegisterUserDataStorage
    {
        private readonly UserProfileContext _context;

        internal RegisterUserDataStorage(UserProfileContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByLogin(Login login)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task Persist(User user)
        {
            await this._context.Users.AddAsync(user);
            await this._context.SaveChangesAsync();
        }
    }
}