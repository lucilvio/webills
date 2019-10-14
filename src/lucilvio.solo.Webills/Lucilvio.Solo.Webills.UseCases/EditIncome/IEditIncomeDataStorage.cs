﻿using Lucilvio.Solo.Webills.Domain.User;
using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UseCases.EditIncome
{
    public interface IEditIncomeDataStorage
    {
        User GetUser();
        Task Persist(Guid incomeNumber, User foundUser);
    }
}