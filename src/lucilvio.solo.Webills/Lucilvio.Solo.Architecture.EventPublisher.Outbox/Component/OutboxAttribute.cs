using System;

namespace Lucilvio.Solo.Architecture.EventPublisher.Outbox.Component
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class OutboxAttribute : Attribute
    {
    }
}
