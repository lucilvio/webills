using Autofac;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure.Injection;
using Lucilvio.Solo.Webills.UserAccount.Login;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.Injection
{
    internal class LoginFactory : AutofacFactory
    {
        public LoginFactory(Module.Configurations configurations, IEventPublisher eventPublisher = null)
            : base(configurations, eventPublisher) { }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UserAccountDataContext(this._configurations)).AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<LoginDataAccess>().As<ILoginDataAccess>().InstancePerLifetimeScope();
            builder.RegisterDecorator<TransactionScopedHandler<LoginMessage>, IUseCase<LoginMessage>>();
            builder.RegisterType<Login.Login>().As<IUseCase<LoginMessage>>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
