using Autofac;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.UserAccount.CreateNewAccount;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure.Injection;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.Injection
{
    internal class CreateNewAccountFactory : AutofacFactory
    {
        public CreateNewAccountFactory(Module.Configurations configurations, IEventPublisher eventPublisher = null)
            : base(configurations, eventPublisher) { }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UserAccountDataContext(base._configurations)).AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<CreateNewAccountDataAccess>().As<ICreateNewAccountDataAccess>().InstancePerLifetimeScope();
            builder.RegisterDecorator<TransactionScopedHandler<CreateNewAccountMessage>, IUseCase<CreateNewAccountMessage>>();

            builder.RegisterType<CreateNewAccount.CreateNewAccount>().As<IUseCase<CreateNewAccountMessage>>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
