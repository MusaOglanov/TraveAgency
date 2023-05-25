using System.Collections.Generic;
using TraveAgency.Models;

namespace TraveAgency.ViewModels
{
    public class KassaVM
    {
        public Income Income { get; set; }
        public SalaryPaid SalaryPaid { get; set; }
        public Expense Expense { get; set; }
        public Kassa Kassa { get; set; }
    }
}
