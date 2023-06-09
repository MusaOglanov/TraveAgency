﻿using System.Collections.Generic;

namespace TraveAgency.Models
{
    public class Kassa
    {
        public int Id { get; set; }
        public decimal Money { get; set; }
        public ICollection<Income> Incomes { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<SalaryPaid> SalaryPaids { get; set; }
    }
}
