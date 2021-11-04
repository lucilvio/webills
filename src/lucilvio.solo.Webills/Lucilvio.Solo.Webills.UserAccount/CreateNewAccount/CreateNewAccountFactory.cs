using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Architecture.EventPublisher.RabbitMq;
using Lucilvio.Solo.Webills.UserAccount.CreateNewAccount;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.Injection
{
    internal class CreateNewAccountFactory : IHandlerFactory<ContainerBuilder>
    {
        public void Create(ContainerBuilder container, object configurations)
        {
            container.Register<DbContext>(c => new UserAccountDataContext((Configurations)configurations)).AsSelf()
                .InstancePerLifetimeScope();

            container.AddRabbitMqEventPublisherWithOutbox(new EventPublisherConfigurations
            {
                Host = "localhost"
            });

            container.RegisterType<CreateNewAccountDataAccess>().AsSelf().InstancePerLifetimeScope();
            container.RegisterDecorator<TransactionScopedHandler<CreateNewAccountMessage>, IMessageHandler<CreateNewAccountMessage>>();
            container.RegisterType<CreateNewAccount.CreateNewAccount>().As<IMessageHandler<CreateNewAccountMessage>>().InstancePerLifetimeScope();
        }
    }
}