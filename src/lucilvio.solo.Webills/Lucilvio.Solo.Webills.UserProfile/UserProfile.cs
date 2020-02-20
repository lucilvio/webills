using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserProfile.DataStorage;
using Lucilvio.Solo.Webills.UserProfile.RegisterUser;

using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.UserProfile
{
    public class UserProfile : IRegisterUser
    {
        private readonly string _connectionString;

        public UserProfile(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task Register(RegisterUserCommand command)
        {
            var dataStorage = new RegisterUserDataStorage(new UserProfileContext(this._connectionString, new DbContextOptions<UserProfileContext>()));
            await new RegisterUser.RegisterUser(dataStorage).Register(command);
        }
    }
}
