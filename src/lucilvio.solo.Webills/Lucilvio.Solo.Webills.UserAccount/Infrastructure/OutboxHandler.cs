using System.Threading.Tasks;
using System.Transactions;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure;

namespace Lucilvio.Solo.Webills.UserAccount.Infrastructure
{
    internal class OutboxHandler<TMessage> : IHandler<TMessage> where TMessage : Message
    {
        private readonly IHandler<TMessage> _innerHandler;

        public OutboxHandler(IHandler<TMessage> innerHandler)
        {
            this._innerHandler = innerHandler;
        }

        public async Task Execute(TMessage message)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await this._innerHandler.Execute(message);
                transaction.Complete();
            }
        }
    }
}