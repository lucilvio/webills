using System;

namespace Lucilvio.Solo.Architecture
{
    public abstract record Message { }

    public abstract record Message<TResponse> : Message
    {
        private TResponse _response;

        public TResponse Response => this._response;
        public bool HasResponse => this._response is not null;

        public void SetResponse(TResponse response)
        {
            if (response is null)
                throw new InvalidOperationException("Message response cannot be null");

            if (this._response != null)
                throw new InvalidOperationException("Message response already setted.");

            this._response = response;
        }
    }

    public abstract record MessageWithAuthorization<TResponse> : Message<TResponse>
    {
        protected MessageWithAuthorization(string[] userRoles)
        {
            this.UserRoles = userRoles;
        }

        public string[] UserRoles { get; }
    }
}