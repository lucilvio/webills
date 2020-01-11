using System;
using System.Runtime.Serialization;

namespace Lucilvio.Solo.Webills.UseCases.RegisterUser
{
    [Serializable]
    internal class LoginAlreadyInUse : Exception
    {
        public LoginAlreadyInUse()
        {
        }

        public LoginAlreadyInUse(string message) : base(message)
        {
        }

        public LoginAlreadyInUse(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LoginAlreadyInUse(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}