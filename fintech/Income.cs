using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fintech
{
    public enum IncomeType
    {
        Salary,
        Scholarship,
        other
    }
    public class Income:Transaction
    {
        public DateTime date { get; set; }
    }
}
