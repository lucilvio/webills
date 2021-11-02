using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.FinancialControl
{
    public interface IFinancialControlModule : IModule { }

    internal class FinancialControlModule : Module, IFinancialControlModule
    {
        public FinancialControlModule(IModuleResolver<IFinancialControlModule> resolver)
            : base(resolver) { }
    }
}