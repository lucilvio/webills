using System;
using System.Runtime.Serialization;

namespace Lucilvio.Solo.Architecture.Handler.Inbox.Component
{
    [Serializable]
    internal class UniqueEventConstraintException : Exception
    {
        public UniqueEventConstraintException()
        {
        }

        public UniqueEventConstraintException(string message) : base(message)
        {
        }

        public UniqueEventConstraintException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UniqueEventConstraintException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}