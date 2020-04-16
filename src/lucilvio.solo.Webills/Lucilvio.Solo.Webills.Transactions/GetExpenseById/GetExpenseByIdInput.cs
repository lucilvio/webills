﻿using System;

namespace Lucilvio.Solo.Webills.Transactions.GetExpense
{
    public class GetExpenseByIdInput
    {
        public GetExpenseByIdInput(Guid userId, Guid id)
        {
            this.UserId = userId;
            this.Id = id;
        }

        internal Guid UserId { get; }
        internal Guid Id { get; }
    }
}