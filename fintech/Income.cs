using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fintech
{
    public class Income:Operation
    {
        public IncomeType IncomeType { get; set; }
        public Money amount { get; set; }
    }
}
