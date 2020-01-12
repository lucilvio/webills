using System;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
{
    public class TodayExpensesData
    {
        public Guid Id { get;  set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int Category { get; set; }
    }
}