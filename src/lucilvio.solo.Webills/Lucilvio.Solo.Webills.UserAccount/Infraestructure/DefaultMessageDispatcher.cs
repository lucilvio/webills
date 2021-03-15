using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.CreateNewAccount;
using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.UserAccount.Login;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure
{
    internal class DefaultMessageDispatcher : IMessageDispatcher
    {
        private static readonly IDictionary<Type, Func<object, Configurations, Task<dynamic>>> _messageHandlersMap;

        static DefaultMessageDispatcher()
        {
            _messageHandlersMap = new Dictionary<Type, Func<object, Configurations, Task<dynamic>>>
            {
                { typeof(LoginMessage), (message, config) => HandleLoginMessage((LoginMessage)message, config) },
                { typeof(CreateNewAccountMessage), (message, config) => HandleCreateNewAccountMessage((CreateNewAccountMessage)message, config) },
                { typeof(GenerateNewPasswordMessage), (message, config) => HandleGenerateNewPasswordMessage((GenerateNewPasswordMessage)message, config) },
            };
        }

        public Task<dynamic> Dispatch(object message, Configurations configurations)
        {
            if (DefaultMessageDispatcher._messageHandlersMap.TryGetValue(message.GetType(), out var handler))
                return handler.Invoke(message, configurations);

            return null;
        }

        private static async Task<dynamic> HandleCreateNewAccountMessage(CreateNewAccountMessage message, Configurations configurations)
        {
            using var ctx = new UserAccountDataContext(configurations);

            ICreateNewAccountDataAccess dataAccess = new CreateNewAccountDataAccess(ctx);
            var handler = new CreateNewAccountMessageHandler(dataAccess);

            return await handler.Execute(message);
        }

        private static async Task<dynamic> HandleGenerateNewPasswordMessage(GenerateNewPasswordMessage message, Configurations configurations)
        {
            using var ctx = new UserAccountDataContext(configurations);

            IGenerateNewPasswordDataAccess dataAccess = new GenerateNewPasswordDataAccess(ctx);
            var handler = new GenerateNewPasswordMessageHandler(dataAccess);

            return await handler.Execute(message);
        }

        private static async Task<dynamic> HandleLoginMessage(LoginMessage message, Configurations configurations)
        {
            using var ctx = new UserAccountDataContext(configurations);

            ILoginDataAccess dataAccess = new LoginDataAccess(ctx);
            var handler = new LoginMessageHandler(dataAccess);

            return await handler.Execute(message);
        }
    }
}