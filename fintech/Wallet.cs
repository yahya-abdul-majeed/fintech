
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fintech
{
    public class Wallet 
    {
        public Guid Id => Guid.NewGuid();

        public Currency currency { get; set; }
        public Money balance { get;set;}     
        public List<Income> incomes { get; set; }   
        public List<Expense> expenses { get; set; }

        public Wallet(Currency currency)
        {
            balance = new Money("0");
            incomes = new List<Income>(); 
            expenses = new List<Expense>();
            this.currency = currency;
        }

        public void AddOperation(Income op)
        {
            incomes.Add(op);
            balance.addMoney(op.amount);
            
            
        }
        public void AddOperation(Expense op)
        {
            expenses.Add(op);
            balance.subtractMoney(op.amount);

        }
        public void CollectStatistics(DateTime from , DateTime to )
        {
            Console.WriteLine("From " + from.Date.ToString("d") + " " + "to " + to.Date.ToString("d")); 

            var incomes = Storage.ActiveWallet.incomes.Where(u => u.Date >= from && u.Date <= to).ToList();
            var expenses = Storage.ActiveWallet.expenses.Where(u => u.Date >= from && u.Date <= to).ToList();


            Console.WriteLine("Income:");
            foreach (var income in incomes)
            {

                Console.WriteLine(income.IncomeType + ":     " + income.amount.DisplayNumAsStr() +" "+Storage.ActiveWallet.currency+"      "+ income.Date.Date.ToString("d"));
            }

            Console.WriteLine("Expense:");
            foreach (var expense in expenses)
            {
                Console.WriteLine(expense.ExpenseType + ":     " + expense.amount.DisplayNumAsStr() +" "+ Storage.ActiveWallet.currency + "      " + expense.Date.Date.ToString("d"));
            }
        }
    }

    
}
