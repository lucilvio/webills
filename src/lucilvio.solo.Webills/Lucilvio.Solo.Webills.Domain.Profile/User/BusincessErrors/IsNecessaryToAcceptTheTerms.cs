using System;
using System.Runtime.Serialization;

namespace Lucilvio.Solo.Webills.Domain.Profile.User.BusinessErrors
{
    [Serializable]
    internal class IsNecessaryToAcceptTheTerms : Exception
    {
        public IsNecessaryToAcceptTheTerms()
        {
        }

        public IsNecessaryToAcceptTheTerms(string message) : base(message)
        {
        }

        public IsNecessaryToAcceptTheTerms(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IsNecessaryToAcceptTheTerms(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}