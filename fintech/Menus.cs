using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fintech
{
    public class Menus
    {

        private Authentication _authentication = new Authentication();
        public void MainMenu()
        {
            Console.WriteLine("1- Register");
            Console.WriteLine("2-Login");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    clearConsole();
                    RegisterMenu();
                    break;
                case "2":
                    clearConsole();
                    LoginMenu();
                    break;
                default:
                    clearConsole();
                    MainMenu();
                    break;
            }
        }

        private void LoginMenu()
        {
            _authentication.LoginUser();
        }

        private void RegisterMenu()
        {
            _authentication.RegisterUser();
        }

        private void clearConsole()
        {
            Console.Clear();
        }
    }
}
