using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UseCases.RegisterUser;
using Lucilvio.Solo.Webills.Domain.Profile.User;

namespace Lucilvio.Solo.Webills.Web.SignUp
{
    public class RegisterUserDataStorageEf : IRegisterUserDataStorage
    {
        private readonly WebillsProfileContext _context;

        public RegisterUserDataStorageEf(WebillsProfileContext context)
        {
            this._context = context;
        }

        public async Task<User> GetUserByLogin(Login login)
        {
            return await this._context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task Persist(User user)
        {
            this._context.Add(user);
            await this._context.SaveChangesAsync();
        }
    }
}