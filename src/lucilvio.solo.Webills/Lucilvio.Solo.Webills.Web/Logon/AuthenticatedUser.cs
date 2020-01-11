﻿using System;

namespace Lucilvio.Solo.Webills.Web.Logon
{
    public class AuthenticatedUser
    {
        public AuthenticatedUser(Guid id, string login, string name)
        {
            this.Id = id;
            this.Login = login;
            this.Name = name;
        }

        public Guid Id { get; }
        public string Login { get; }
        public string Name { get; }
    }
}