﻿using System.Threading.Tasks;
using System.Transactions;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure;

namespace Lucilvio.Solo.Webills.UserAccount.Infrastructure
{
    internal class TransactionScopedHandler<TMessage> : IUseCase<TMessage> where TMessage : Message
    {
        private readonly IUseCase<TMessage> _innerHandler;

        public TransactionScopedHandler(IUseCase<TMessage> innerHandler)
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