using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Lucilvio.Solo.Webills.UserAccount.Login;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal class DefaultMessageResolver : IMessageResolver
    {
        private readonly IEventBus _eventBus;
        private readonly IDictionary<Module.Messages, Func<object, Task>> _messagesMap;

        public DefaultMessageResolver(IEventBus eventBus, Module.Configurations configs)
        {
            this._messagesMap = new Dictionary<Module.Messages, Func<object, Task>>
            {
                { Module.Messages.Login, this.ExecuteLoginComponent }
            };

            this._eventBus = eventBus;
        }

        public async Task Resolve(Module.Messages message, object input)
        {
            if (!this._messagesMap.TryGetValue(message, out var action))
                return;

            await action.Invoke(input);
        }

        internal async Task ExecuteLoginComponent(object input)
        {
            var context = new UserAccountContext();
            var loginDataAccess = new LoginDataAccess(context);

            var component = new LoginComponent(loginDataAccess, this._eventBus);

            await component.Execute(input);
        }
    }

    
}
