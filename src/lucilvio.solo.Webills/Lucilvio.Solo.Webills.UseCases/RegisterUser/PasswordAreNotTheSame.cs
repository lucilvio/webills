using System;
using System.Runtime.Serialization;

namespace Lucilvio.Solo.Webills.UseCases.RegisterUser
{
    [Serializable]
    internal class PasswordAreNotTheSame : Exception
    {
        public PasswordAreNotTheSame()
        {
        }

        public PasswordAreNotTheSame(string message) : base(message)
        {
        }

        public PasswordAreNotTheSame(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PasswordAreNotTheSame(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}