using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class SearchForUserTransactionsInformationResult
    {
        private SearchForUserTransactionsInformationResult()
        {
            this.Incomes = new List<UserIncomeData>();
            this.Expenses = new List<UserExpenseData>();
        }

        public SearchForUserTransactionsInformationResult(decimal balance, decimal totalSpent, IEnumerable<UserIncomeData> incomes, 
            IEnumerable<UserExpenseData> expenses)
        {
            this.Balance = balance;
            this.Incomes = incomes;
            this.Expenses = expenses;
            this.TotalSpent = totalSpent;
        }

        public static SearchForUserTransactionsInformationResult Empty => new SearchForUserTransactionsInformationResult();

        public decimal Balance { get; }
        public decimal TotalSpent { get; }
        public IEnumerable<UserIncomeData> Incomes { get; set; }
        public IEnumerable<UserExpenseData> Expenses { get; set; }
    }
}