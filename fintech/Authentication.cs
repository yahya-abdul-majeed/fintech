
using fintech.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fintech
{
    public class Authentication
    {
        private readonly JsonService JS = new JsonService();

        public Authentication()
        {
            Storage.Users = JS.GetUsersFromJSON();
        }

        public bool RegisterUser()
        {
            string name = "";
            string email = "";
            string password = "";
            bool check;
            do
            {
                check = false;
                Console.WriteLine("Enter Name:");
                name = Console.ReadLine();
                check = SD.Namereg.IsMatch(name);
            } while (!check);
            do
            {
                check = false;
                Console.WriteLine("Enter Email:");
                email = Console.ReadLine();
                check = SD.Emailreg.IsMatch(email);

            } while (!check);
            do
            {
                check = false;
                Console.WriteLine("Enter password");
                password = Console.ReadLine();
                if (!String.IsNullOrEmpty(password))
                {
                    check = true;
                }
            } while (!check);


            var newUser = new User(name,email, password);

            Storage.Users.Add(newUser);
            Storage.UpdateLists();
            Console.ReadLine();
            return true;
        }

        public bool LoginUser()
        {
            string email = "";
            string password = "";
            bool check;
            do
            {
                check = false;
                Console.WriteLine("Enter Email:");
                email = Console.ReadLine();
                check = SD.Emailreg.IsMatch(email);

            } while (!check);
            do
            {
                check = false;
                Console.WriteLine("Enter password");
                password = Console.ReadLine();
                if (!String.IsNullOrEmpty(password))
                {
                    check = true;
                }
            } while (!check);

            var user = Storage.Users.FirstOrDefault(u => u.Email == email);
            if (user is not null)
            {
                if (user.PasswordHash == password)
                {
                    //user logged in
                    Console.WriteLine("user logged in");
                    Storage.ActiveUser = user;
                    return true;
                }
                else
                {
                    Console.WriteLine("wrong password");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("such user does not exist");
                return false;
            }
        }
        public bool Logout()
        {
            if(Storage.ActiveUser != null)
            {
                Storage.ActiveUser = null;
                Storage.ActiveWallet = null;
                return true;
            }
            return false;
        }
    }
}
