using Autofac;
using Lucilvio.Solo.Webills.Notification.NotifyAccountCreation;

namespace Lucilvio.Solo.Webills.Notification.Infrastructure.AutofacModule
{
    internal class NotifyAccountCreationAutofacModule : Autofac.Module
    {
        private readonly Module.Configurations _configurations;

        public NotifyAccountCreationAutofacModule(Module.Configurations configurations)
        {
            this._configurations = configurations ?? throw new System.ArgumentNullException(nameof(configurations));
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NotifyAccountCreation.NotifyAccountCreation>().As<IHandler<AccountCreatedMessage>>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
