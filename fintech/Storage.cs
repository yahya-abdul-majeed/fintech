using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fintech
{
    public static class Storage
    {
        public static List<User> Users { get; set; }
        public static User? ActiveUser { get; set; }
        public static Wallet? ActiveWallet { get; set; }
    
        

        public static void UpdateLists()
        {
            JsonService JS = new JsonService();
            JS.WriteUsersToJSON(Users);
        }
    }
}
