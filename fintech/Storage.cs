using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fintech
{
    public class Storage
    {
        private readonly JsonService JS = new JsonService();
        public List<User> Users { get; set; }
        public User ActiveUser { get; set; }

        public Storage()
        {
            Users = JS.GetUsersFromJSON();
        }

        public void UpdateLists()
        {
            JsonService JS = new JsonService();
            JS.WriteUsersToJSON(Users);
        }
    }
}
