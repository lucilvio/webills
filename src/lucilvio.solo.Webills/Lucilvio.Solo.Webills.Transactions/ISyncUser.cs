using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions
{
    public interface ISyncUser
    {
        Task Execute(Guid id);
    }
}