using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fintech
{
    public class Expense:Operation
    {
        public ExpenseType ExpenseType { get; set; }
        public Money amount { get; set; }
    }
}
