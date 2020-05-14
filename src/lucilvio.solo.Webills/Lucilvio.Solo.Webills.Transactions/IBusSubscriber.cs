using System;

namespace Lucilvio.Solo.Webills.Transactions
{
    internal interface IBusSubscriber
    {
        void Subscribe<TEvent>(Action<TEvent> action);
    }
}