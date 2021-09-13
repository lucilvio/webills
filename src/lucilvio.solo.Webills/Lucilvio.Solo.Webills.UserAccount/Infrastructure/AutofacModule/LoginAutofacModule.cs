using Autofac;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure;
using Lucilvio.Solo.Webills.UserAccount.Login;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.Injection
{
    internal class LoginAutofacModule : Autofac.Module
    {
        private readonly Module.Configurations _configurations;

        public LoginAutofacModule(Module.Configurations configurations)
        {
            this._configurations = configurations;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UserAccountDataContext(this._configurations)).AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<LoginDataAccess>().As<ILoginDataAccess>().InstancePerLifetimeScope();
            builder.RegisterDecorator<OutboxHandler<LoginMessage>, IHandler<LoginMessage>>();
            builder.RegisterType<Login.Login>().As<IHandler<LoginMessage>>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
