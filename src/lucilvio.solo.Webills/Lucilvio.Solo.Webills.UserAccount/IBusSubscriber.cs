using System;
using System.Collections.Generic;
using System.Linq;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal interface IBusSubscriber
    {
        void Subscribe<TEvent>(Action<TEvent> action);
    }
}