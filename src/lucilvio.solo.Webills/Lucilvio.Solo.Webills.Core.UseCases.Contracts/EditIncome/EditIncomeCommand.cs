﻿using Lucilvio.Solo.Webills.Core.Domain.User;
using System;

namespace Lucilvio.Solo.Webills.Core.UseCases.Contracts.EditIncome
{
    public abstract class EditIncomeCommand
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public DateTime Date { get; protected set; }
        public TransactionValue Value { get; protected set; }
    }
}