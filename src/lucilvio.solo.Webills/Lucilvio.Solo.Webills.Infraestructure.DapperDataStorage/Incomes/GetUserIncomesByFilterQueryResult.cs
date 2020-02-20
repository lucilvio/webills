﻿using System.Collections.Generic;
using System.Linq;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
{
    public class GetUserIncomesByFilterQueryResult
    {
        private GetUserIncomesByFilterQueryResult()
        {
            this.Incomes = new List<UserIncomeData>();
        }

        public GetUserIncomesByFilterQueryResult(IEnumerable<UserIncomeData> incomes) : this()
        {
            if (incomes != null)
                this.Incomes = incomes;
        }

        public IEnumerable<UserIncomeData> Incomes { get; private set; }

        public bool HasIncomes => this.Incomes != null && this.Incomes.Any();
    }
}