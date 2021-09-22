using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure.Injection;
using Lucilvio.Solo.Webills.UserAccount.Login;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.Injection
{
    internal class LoginFactory : AutofacFactory
    {
        public LoginFactory(Module.Configurations configurations) : base(configurations) { }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UserAccountDataContext(this._configurations)).AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<LoginDataAccess>().As<ILoginDataAccess>().InstancePerLifetimeScope();
            builder.RegisterDecorator<TransactionScopedHandler<LoginMessage>, IHandler<LoginMessage>>();
            builder.RegisterType<Login.Login>().As<IHandler<LoginMessage>>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}