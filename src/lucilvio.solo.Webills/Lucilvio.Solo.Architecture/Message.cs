namespace Lucilvio.Solo.Architecture
{
    public abstract record Message { }

    public abstract record Message<TResponse> : Message
    {
        private TResponse _response;

        public TResponse Response => this._response;
        public void SetResponse(TResponse response) => this._response = response;
    }
}