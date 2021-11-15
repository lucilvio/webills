using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture.Handler.Inbox.Component
{
    internal interface IInboxDataAccess
    {
        Task<bool> EventHasBeenReceived(Guid id);
        Task PersistIncomingEvent(IncomingEvent incomingEvent);
        Task UpdateEventStatus(IncomingEvent newIncomingEvent);
    }
}