using System;

namespace Lucilvio.Solo.Architecture
{
    public class Error : Exception
    {
        public Error() { }
        public Error(string message) : base(message) { }
    }
}