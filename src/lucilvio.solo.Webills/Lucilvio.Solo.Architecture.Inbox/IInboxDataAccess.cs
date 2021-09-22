﻿using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture.Inbox
{
    public interface IInboxDataAccess
    {
        Task<bool> EventHasBeenReceived(Guid id);
        Task PersistIncomingEvent(IncomingEvent incomingEvent);
        Task UpdateEventStatus(IncomingEvent newIncomingEvent);
    }
}