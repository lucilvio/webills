using System;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public class BusinessError : Exception
    {
        public BusinessError() { }
        public BusinessError(string message) : base(message) { }
        public BusinessError(string message, Exception innerException) : base(message, innerException) { }
    }
}