using System.Collections.Generic;
using System.Linq;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class SearchForUserTransactionsInformationResult
    {
        private SearchForUserTransactionsInformationResult()
        {
            this.Incomes = new List<UserIncomeData>();
            this.Expenses = new List<UserExpenseData>();
        }

        public SearchForUserTransactionsInformationResult(decimal balance, decimal totalIncomes, decimal totalSpent, 
            IEnumerable<UserIncomeData> incomes, IEnumerable<UserExpenseData> expenses)
        {
            this.Balance = balance;
            this.TotalSpent = totalSpent;
            this.TotalIncomes = totalIncomes;
            this.Incomes = incomes.OrderByDescending(i => i.Date);
            this.Expenses = expenses.OrderByDescending(e => e.Date);
        }

        public static SearchForUserTransactionsInformationResult Empty => new SearchForUserTransactionsInformationResult();

        public decimal Balance { get; }
        public decimal TotalSpent { get; }
        public decimal TotalIncomes { get; internal set; }
        public IEnumerable<UserIncomeData> Incomes { get; set; }
        public IEnumerable<UserExpenseData> Expenses { get; set; }
    }
}