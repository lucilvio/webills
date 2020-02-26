using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.CreateUser
{
    internal class CreateUserComponent : IComponent
    {
        private readonly ICreateUserDataAccess _dataAccess;

        public CreateUserComponent(ICreateUserDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public async Task Execute(ICreateUserInput command)
        {
            var newUser = new User(command.Id);
            await this._dataAccess.Persist(newUser);
        }
    }
}
