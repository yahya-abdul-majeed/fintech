using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fintech
{
    public enum ExpenseType{
        food,
        medicine,
        restaurant
    }
    public class Expense:Transaction
    {
        public ExpenseType expenseType { get;set; }
    }
}
