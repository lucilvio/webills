using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Dashboard.AddExpense;
using Lucilvio.Solo.Webills.Dashboard.EditTransaction;
using Lucilvio.Solo.Webills.Dashboard.GetUserDashboardInfo;
using Lucilvio.Solo.Webills.Dashboard.RemoveTransaction;

namespace Lucilvio.Solo.Webills.Dashboard
{
    public class DashboardModule
    {
        private readonly DependencyResolverWithSimpleInjector _dependencyResolver;

        public DashboardModule()
        {
            this._dependencyResolver = new DependencyResolverWithSimpleInjector();
        }

        public async Task AddTransaction(AddTransactionInput input)
        {
            this.VerifyInput(input);

            var component = this._dependencyResolver.Container.GetInstance<AddTransactionComponent>();
            await component.Execute(input);
        }


        public async Task EditTransaction(EditTransactionInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            var component = this._dependencyResolver.Container.GetInstance<EditTransactionComponent>();
            await component.Execute(input);
        }

        public async Task RemoveTransaction(RemoveTransactionInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            var component = this._dependencyResolver.Container.GetInstance<RemoveTransactionComponent>();
            await component.Execute(input);
        }

        public async Task<GetDashboardInfoByFilterOutput> GetDashboardInfoByFilter(GetDashboardInfoByFilterInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            var component = this._dependencyResolver.Container.GetInstance<GetDashboardInfoByFilterComponent>();
            return await component.Execute(input);
        }

        private void VerifyInput(object input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();
        }
        
        internal class Error
        {
            public class ComponentInputNotInformed : Exception { }
        }

    }
}