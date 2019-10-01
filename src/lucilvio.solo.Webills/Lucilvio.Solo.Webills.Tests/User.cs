using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lucilvio.Solo.Webills.Tests
{
    internal class User
    {
        private readonly List<Income> _incomes;
        private readonly List<Expanse> _expanses;

        public User()
        {
            this._incomes = new List<Income>();
            this._expanses = new List<Expanse>();
        }

        public ReadOnlyCollection<Income> Incomes => this._incomes.AsReadOnly();

        internal void AddExpanse(Expanse expanse)
        {
            if (expanse == null)
                throw new UserCannotAddNullExpanse();

            this._expanses.Add(expanse);
        }

        public ReadOnlyCollection<Expanse> Expanses => this._expanses.AsReadOnly();


        public bool HasIncomes => this._incomes.Any();
        public bool HasExpanses => this._expanses.Any();

        public decimal Balance => this.Incomes.Sum(i => i.Value.Value) - this.Expanses.Sum(e => e.Value.Value);

        internal void AddIncome(Income income)
        {
            if (income == null)
                throw new UserCannotAddNullIncome();

            this._incomes.Add(income);
        }
    }
}