﻿using System;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewIncome;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class AddNewIncomeCommandAdapter : AddNewIncomeCommand
    {
        public AddNewIncomeCommandAdapter(AddNewIncomeViewModel viewModel)
        {
            if (viewModel == null)
                return;

            this.Name = viewModel.Name;
            this.Date = !string.IsNullOrEmpty(viewModel.Date) ? DateTime.Parse(viewModel.Date) : DateTime.MinValue;
            this.Value = !string.IsNullOrEmpty(viewModel.Value) ? new TransactionValue(viewModel.Value.MoneyToDecimal()) : TransactionValue.Zero;
        }
    }
}