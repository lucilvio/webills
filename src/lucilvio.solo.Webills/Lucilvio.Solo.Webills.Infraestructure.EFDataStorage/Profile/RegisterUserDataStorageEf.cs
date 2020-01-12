using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Lucilvio.Solo.Webills.Profile.Domain.User;
using Lucilvio.Solo.Webills.Profile.UseCases.RegisterUser;

namespace Lucilvio.Solo.Webills.Infraestructure.EFDataStorage.Profile
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