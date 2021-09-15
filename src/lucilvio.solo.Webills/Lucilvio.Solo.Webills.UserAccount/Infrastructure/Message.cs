namespace Lucilvio.Solo.Webills.UserAccount.Infrastructure
{
    public abstract record Message { }

    public abstract record Message<TResponse> : Message
    {
        private TResponse _response;

        public TResponse Response => this._response;
        internal void SetResponse(TResponse response) => this._response = response;
    }
}