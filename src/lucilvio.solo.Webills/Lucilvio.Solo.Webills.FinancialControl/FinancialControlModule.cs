using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.FinancialControl
{
    public interface IFinancialControlModule : IMessageSender { }

    internal class FinancialControlModule : IFinancialControlModule
    {
        private readonly IModuleResolver<IFinancialControlModule> _moduleResolver;

        public FinancialControlModule(IModuleResolver<IFinancialControlModule> moduleResolver)
        {
            this._moduleResolver = moduleResolver;
        }

        public async Task SendMessage(Message message)
        {
            await this._moduleResolver.Resolve(message);
        }
    }
}