using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture.Handler.Inbox
{
    internal class LogHandler<TMessage> : IMessageHandler<TMessage> where TMessage : Message
    {
        private readonly IMessageHandler<TMessage> _innerHandler;

        public LogHandler(IMessageHandler<TMessage> innerHandler)
        {
            this._innerHandler = innerHandler;
        }

        public async Task Execute(TMessage message)
        {
            System.Console.WriteLine("Begining");

            if (this._innerHandler is not null)
                await this._innerHandler.Execute(message);

            System.Console.WriteLine("End");
        }
    }
}