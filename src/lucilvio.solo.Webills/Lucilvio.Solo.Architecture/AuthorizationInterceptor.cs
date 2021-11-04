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
            if (this.MessageHasAllowedRoles(message))
                if (!this.IsUserAuthorized(message))
                    throw new UnauthorizedError();

            await this._innerMessageHandler.Execute(message);
        }

        private bool MessageHasAllowedRoles(TMessage message)
        {
            var allowedRolesAttribute = message.GetType()
                .GetCustomAttributes(typeof(AllowedRoles), false);

            return allowedRolesAttribute.Length > 0;
        }

        private bool IsUserAuthorized(TMessage message)
        {
            var allowedRoles = this.GetAllowedRoles(message);
            var userRoles = this.GetUserRoles(message);

            if (this.UserHasTheAllowedRoles(allowedRoles, userRoles))
                return true;

            return false;
        }

        private string[] GetAllowedRoles(TMessage message)
        {
            var allowedRoles = message.GetType()
                .GetCustomAttributes(typeof(AllowedRoles), false)
                .FirstOrDefault() as AllowedRoles;

            return allowedRoles != null ? allowedRoles.Roles.ToArray() : Array.Empty<string>();
        }

        private string[] GetUserRoles(TMessage message)
        {
            var userRolesProperty = message.GetType().GetProperty("UserRoles");

            if (userRolesProperty is null)
                return Array.Empty<string>();

            return (string[])userRolesProperty.GetValue(message);
        }

        private bool UserHasTheAllowedRoles(IReadOnlyCollection<string> allowedRoles, string[] userRoles)
        {
            if (userRoles.All(v => allowedRoles.Contains(v)))
                return true;

            return false;
        }
    }
}