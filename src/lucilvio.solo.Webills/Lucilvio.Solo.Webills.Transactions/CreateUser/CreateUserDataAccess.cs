using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;
using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Transactions.CreateUser
{
    internal class CreateUserDataAccess : ICreateUserDataAccess
    {
        private readonly TransactionsContext _context;

        public CreateUserDataAccess(TransactionsContext context)
        {
            _context = context;
        }

        public async Task Persist(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}