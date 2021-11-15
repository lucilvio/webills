using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture.EventPublisher.Outbox.Component
{
    internal interface IOutboxDataAccess
    {
        Task PersistEvent(OutgoingEvent outgoingEvent);
    }
}