﻿namespace Lucilvio.Solo.Webills.Web.Home
{
    public class AddNewExpenseRequest
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Value { get; set; }
        public string Category { get; set; }
    }
}