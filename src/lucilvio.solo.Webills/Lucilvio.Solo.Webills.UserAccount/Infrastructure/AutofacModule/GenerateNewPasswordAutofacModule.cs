using Autofac;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.Injection
{
    internal class GenerateNewPasswordAutofacModule : Autofac.Module
    {
        private readonly Module.Configurations _configurations;
        private readonly IEventBus _eventBus;

        public GenerateNewPasswordAutofacModule(Module.Configurations configurations, IEventBus eventBus)
        {
            this._configurations = configurations ?? throw new System.ArgumentNullException(nameof(configurations));
            this._eventBus = eventBus ?? throw new System.ArgumentNullException(nameof(eventBus));
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UserAccountDataContext(this._configurations)).AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterDecorator<TransactionScopedHandler<GenerateNewPasswordMessage>, IHandler<GenerateNewPasswordMessage>>();
            builder.RegisterType<GenerateNewPasswordDataAccess>().As<IGenerateNewPasswordDataAccess>().InstancePerLifetimeScope();
            builder.RegisterType<GenerateNewPassword.GenerateNewPassword>().As<IHandler<GenerateNewPasswordMessage>>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
