using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture.Inbox
{
    internal class LogHandler<TMessage> : IHandler<TMessage> where TMessage : Message
    {
        private readonly IHandler<TMessage> _innerHandler;

        public LogHandler(IHandler<TMessage> innerHandler)
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