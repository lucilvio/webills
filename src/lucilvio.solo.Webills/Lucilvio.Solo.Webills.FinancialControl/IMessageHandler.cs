using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.FinancialControl
{
    internal interface IMessageHandler<in TMessage>
    {
        Task<dynamic> Execute(TMessage message);
    }
}