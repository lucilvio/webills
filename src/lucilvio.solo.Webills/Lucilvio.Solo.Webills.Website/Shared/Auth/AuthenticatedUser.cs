using System;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.Website.Shared.Authorization
{
    public class AuthenticatedUser
    {
        public AuthenticatedUser(Guid id, string name, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;

            this.Roles = Array.AsReadOnly(new string[] { "GetFinancialInformation" });
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
        public IReadOnlyCollection<string> Roles { get; }
    }
}