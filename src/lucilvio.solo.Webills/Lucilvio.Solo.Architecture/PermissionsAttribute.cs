using System;
using System.Collections.Generic;

namespace Lucilvio.Solo.Architecture
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AllowedRoles : Attribute
    {
        private readonly string[] _roles;

        public AllowedRoles(params string[] roles)
        {
            this._roles = roles;
        }

        public IReadOnlyCollection<string> Roles => this._roles;
    }
}
