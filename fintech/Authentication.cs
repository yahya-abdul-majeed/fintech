
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
        private readonly Storage storage = new Storage();

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

            storage.Users.Add(newUser);
            storage.UpdateLists();
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

            var user = storage.Users.FirstOrDefault(u => u.Email == email);
            if (user is not null)
            {
                if (user.PasswordHash == password)
                {
                    //user logged in
                    Console.WriteLine("user logged in");
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
    }
}
