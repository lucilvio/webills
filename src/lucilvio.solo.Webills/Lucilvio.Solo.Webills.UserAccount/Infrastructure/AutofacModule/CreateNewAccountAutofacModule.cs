using Autofac;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.UserAccount.CreateNewAccount;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.Injection
{
    internal class CreateNewAccountAutofacModule : Autofac.Module
    {
        private readonly Module.Configurations _configurations;
        private readonly IEventBus _eventBus;

        public CreateNewAccountAutofacModule(Module.Configurations configurations, IEventBus eventBus = null)
        {
            this._configurations = configurations;
            this._eventBus = eventBus;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UserAccountDataContext(this._configurations)).AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<CreateNewAccountDataAccess>().As<ICreateNewAccountDataAccess>().InstancePerLifetimeScope();
            builder.RegisterDecorator<TransactionScopedHandler<CreateNewAccountMessage>, IHandler<CreateNewAccountMessage>>();
            
            builder.RegisterType<CreateNewAccount.CreateNewAccount>().As<IHandler<CreateNewAccountMessage>>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
