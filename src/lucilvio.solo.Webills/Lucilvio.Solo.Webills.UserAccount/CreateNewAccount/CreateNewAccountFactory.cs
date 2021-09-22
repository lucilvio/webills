using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Architecture.Outbox;
using Lucilvio.Solo.Webills.EventBus.RabbitMq;
using Lucilvio.Solo.Webills.UserAccount.CreateNewAccount;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure.Injection;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.Injection
{
    internal class CreateNewAccountFactory : AutofacFactory
    {
        public CreateNewAccountFactory(Module.Configurations configurations) : base(configurations) { }

        protected override void Load(ContainerBuilder builder)
        {            
            builder.Register<DbContext>(c => new UserAccountDataContext(base._configurations)).AsSelf()
                .InstancePerLifetimeScope();

            builder.AddRabbitMqEventPublisher(new Configurations
            {
                Host = "localhost"
            }).RegisterOutbox();

            builder.RegisterType<CreateNewAccountDataAccess>().As<ICreateNewAccountDataAccess>().InstancePerLifetimeScope();
            builder.RegisterDecorator<TransactionScopedHandler<CreateNewAccountMessage>, IHandler<CreateNewAccountMessage>>();
            builder.RegisterType<CreateNewAccount.CreateNewAccount>().As<IHandler<CreateNewAccountMessage>>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}