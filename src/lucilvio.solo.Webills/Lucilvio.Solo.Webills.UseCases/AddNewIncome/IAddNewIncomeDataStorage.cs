﻿using Lucilvio.Solo.Webills.Domain.User;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UseCases.AddNewIncome
{
    public interface IAddNewIncomeDataStorage
    {
        Task<User> GetUser();
        Task Persist(User user);
    }
}