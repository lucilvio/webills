using System;
using Lucilvio.Solo.Webills.Core.Domain.User;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
{
    public class UserExpenseData
    {
        public Guid Id { get; internal set; }
        public string Name { get; internal set; }
        public DateTime Date { get; internal set; }
        public decimal Value { get; internal set; }
        public int Category { get; internal set; }
    }
}