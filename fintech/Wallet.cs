
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fintech
{
    public class Wallet 
    {
        public Guid Id => new Guid();

        public string currency { get; set; }
        public double balance { get;set;}
        public List<Transaction> transactions { get; set; }

        public Wallet(string currency)
        {
            balance = 0;
            transactions = new List<Transaction>();
            this.currency = currency;
        }
    }
}
