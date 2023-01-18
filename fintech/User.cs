
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fintech
{
    public class User
    {
        public string Name;
        public string Email;
        public string PasswordHash;
        public List<Wallet> Wallets;

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            PasswordHash = password;
            Wallets = new List<Wallet>
            {
                new Wallet("usd")
            };
        }
    }
}
