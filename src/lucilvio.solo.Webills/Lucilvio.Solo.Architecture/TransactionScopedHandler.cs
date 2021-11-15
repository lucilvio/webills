using System.Threading.Tasks;
using System.Transactions;

namespace Lucilvio.Solo.Architecture
{
    public class TransactionScopedHandler<TMessage> : IMessageHandler<TMessage> where TMessage : Message
    {
        private readonly IMessageHandler<TMessage> _innerHandler;

        public TransactionScopedHandler(IMessageHandler<TMessage> innerHandler)
        {
            this._innerHandler = innerHandler;
        }

        public async Task Execute(TMessage message)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await this._innerHandler.Execute(message);

            transaction.Complete();
        }
    }
}