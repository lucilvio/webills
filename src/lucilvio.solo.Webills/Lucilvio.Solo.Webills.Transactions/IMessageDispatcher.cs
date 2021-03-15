using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.FinancialControl
{
    internal interface IMessageDispatcher<in TMessage>
    {
        Task<dynamic> Dispatch(TMessage message, Configurations configurations = null);
    }
}