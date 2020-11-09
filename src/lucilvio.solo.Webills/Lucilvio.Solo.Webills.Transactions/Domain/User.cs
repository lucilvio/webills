﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    internal class User
    {
        private readonly IList<Income> _incomes;
        private readonly IList<Expense> _expenses;
        private readonly IList<RecurrentExpense> _recurrentExpenses;

        internal User(Guid id)
        {
            if (id == Guid.Empty)
                throw new Error.CantCreateUserWithoutId();

            this.Id = id;

            this._incomes = new List<Income>();
            this._expenses = new List<Expense>();
            this._recurrentExpenses = new List<RecurrentExpense>();
        }

        internal Guid Id { get; }
        internal IEnumerable<Income> Incomes => this._incomes;

        internal IEnumerable<Expense> Expenses => this._expenses;
        internal IEnumerable<RecurrentExpense> RecurrentExpenses => this._recurrentExpenses;

        public Income AddIncome(string name, DateTime date, Income.IncomeCategory category, TransactionValue value)
        {
            var newIncome = new Income(name, date, category, value);
            this._incomes.Add(newIncome);

            return newIncome;
        }

        public Expense AddExpense(string name, Expense.ExpenseCategory category, DateTime date, TransactionValue value)
        {
            var newExpense = Expense.New(name, category, date, value);
            this._expenses.Add(newExpense);

            return newExpense;
        }

        public void AddRecurrentExpense(string name, Expense.ExpenseCategory category, DateTime date, TransactionValue value,
            Recurrency recurrency)
        {
            this._recurrentExpenses.Add(new RecurrentExpense(name, category, date, value, recurrency));
        }

        public Income EditIncome(Guid id, string name, DateTime date, TransactionValue value)
        {
            var foundIncome = this._incomes.FirstOrDefault(i => i.Id == id);

            if (foundIncome == null)
                throw new Error.IncomeNotFound();

            foundIncome.Change(name, date, value);

            return foundIncome;
        }

        public Expense EditExpense(Guid id, string name, Expense.ExpenseCategory category, DateTime date, TransactionValue value)
        {
            var foundExpense = this._expenses.FirstOrDefault(e => e.Id == id);

            if (foundExpense == null)
                throw new Error.ExpenseNotFound();

            foundExpense.Change(name, category, date, value);

            return foundExpense;
        }

        public void RemoveExpense(Guid expenseNumber)
        {
            var foundExpense = this._expenses.FirstOrDefault(e => e.Id == expenseNumber);

            if (foundExpense == null)
                throw new Error.ExpenseNotFound();

            this._expenses.Remove(foundExpense);
        }

        public void RemoveIncome(Guid incomeNumber)
        {
            var foundIncome = this._incomes.FirstOrDefault(i => i.Id == incomeNumber);

            if (foundIncome == null)
                throw new Error.IncomeNotFound();

            this._incomes.Remove(foundIncome);
        }

        internal class Error
        {
            internal class ExpenseNotFound : Exception { }
            internal class IncomeNotFound : Exception { }
            internal class CantCreateUserWithoutId : Exception { }
        }
    }
}