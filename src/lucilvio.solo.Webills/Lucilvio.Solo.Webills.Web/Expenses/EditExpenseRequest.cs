﻿using System;

namespace Lucilvio.Solo.Webills.Clients.Web.Expenses
{
    public class EditExpenseRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
        public string Value { get; set; }
    }
}