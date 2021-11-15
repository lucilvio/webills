using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture
{
    public class AuthorizationInterceptor<TMessage> : IMessageHandler<TMessage> where TMessage : Message
    {
        private readonly IMessageHandler<TMessage> _innerMessageHandler;

        public AuthorizationInterceptor(IMessageHandler<TMessage> messageHandler)
        {
            this._innerMessageHandler = messageHandler ?? throw new System.ArgumentNullException(nameof(messageHandler));
        }

        public async Task Execute(TMessage message)
        {
            if (MessageHasAllowedRoles(message) && !IsUserAuthorized(message))
                throw new UnauthorizedError();

            await this._innerMessageHandler.Execute(message);
        }

        private static bool MessageHasAllowedRoles(TMessage message)
        {
            var allowedRolesAttribute = message.GetType()
                .GetCustomAttributes(typeof(AllowedRolesAttribute), false);

            return allowedRolesAttribute.Length > 0;
        }

        private static bool IsUserAuthorized(TMessage message)
        {
            var allowedRoles = GetAllowedRoles(message);
            var userRoles = GetUserRoles(message);

            return UserHasTheAllowedRoles(allowedRoles, userRoles);
        }

        private static IReadOnlyCollection<string> GetAllowedRoles(TMessage message)
        {
            var allowedRoles = message.GetType()
                .GetCustomAttributes(typeof(AllowedRolesAttribute), false)
                .FirstOrDefault() as AllowedRolesAttribute;

            return allowedRoles != null ? allowedRoles.Roles : Array.Empty<string>();
        }

        private static IReadOnlyCollection<string> GetUserRoles(TMessage message)
        {
            var userRolesProperty = message.GetType().GetProperty(nameof(MessageWithAuthorization<object>.UserRoles));

            if (userRolesProperty is null)
                return Array.Empty<string>();

            return userRolesProperty.GetValue(message) as IReadOnlyCollection<string>;
        }

        private static bool UserHasTheAllowedRoles(IReadOnlyCollection<string> allowedRoles,
            IReadOnlyCollection<string> userRoles)
        {
            return userRoles.All(v => allowedRoles.Contains(v));
        }
    }
}