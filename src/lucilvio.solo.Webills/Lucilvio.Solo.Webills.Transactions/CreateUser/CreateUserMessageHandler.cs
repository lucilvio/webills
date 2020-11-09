using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.CreateUser
{
    internal class CreateUserMessageHandler
    {
        private readonly ICreateUserDataAccess _dataAccess;

        public CreateUserMessageHandler(ICreateUserDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public async Task Execute(CreateUserMessage message)
        {
            var newUser = new User(message.UserId);
            await this._dataAccess.Persist(newUser);
        }
    }
}
