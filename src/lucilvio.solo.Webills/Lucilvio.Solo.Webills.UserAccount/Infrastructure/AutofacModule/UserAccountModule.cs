using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure;

namespace Lucilvio.Solo.Webills.UserAccount.Infrastructure.AutofacModule
{
    public class UserAccountModule : Module
    {
        private readonly IContainer _container;

        public UserAccountModule(Configurations configurations) : base(configurations)
        {
            this._container = this.BuildContainer(configurations);
        }

        public override async Task SendMessage(Message message)
        {
            using (var scope = this._container.BeginLifetimeScope())
            {
                try
                {
                    Type handlerType = typeof(IHandler<>).MakeGenericType(message.GetType());
                    dynamic handler = scope.Resolve(handlerType);

                    await handler.Execute((dynamic)message);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private IContainer BuildContainer(Configurations configurations)
        {
            var builder = new ContainerBuilder();
            builder = this.BuildAutofacModules(builder, configurations);

            return builder.Build();
        }

        private ContainerBuilder BuildAutofacModules(ContainerBuilder builder, Configurations configurations)
        {
            this.GetType().Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(Autofac.Module)))
                .Select(m => (Autofac.Module)Activator.CreateInstance(m, configurations))
                .ToList().ToList().ForEach(m => builder.RegisterModule(m));

            return builder;
        }
    }
}