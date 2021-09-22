using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture.Outbox
{
    internal interface IOutboxDataAccess
    {
        Task PersistEvent(OutgoingEvent outgoingEvent);
    }
}