﻿namespace Lucilvio.Solo.Webills.UserAccount.Infrastructure.Injection
{
    internal abstract class AutofacFactory : Autofac.Module
    {
        protected readonly Module.Configurations _configurations;

        public AutofacFactory(Module.Configurations configurations)
        {
            this._configurations = configurations;
        }
    }
}
