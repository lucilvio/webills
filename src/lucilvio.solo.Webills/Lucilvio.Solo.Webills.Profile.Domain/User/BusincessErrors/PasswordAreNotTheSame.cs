using System;
using System.Runtime.Serialization;

namespace Lucilvio.Solo.Webills.Profile.Domain.User.BusinessErrors
{
    internal class PasswordAreNotTheSame : Exception
    {
        public PasswordAreNotTheSame()
        {
        }
    }
}