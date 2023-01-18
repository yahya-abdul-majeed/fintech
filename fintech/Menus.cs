using fintech.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private void userMenu() //after user is logged in 
        {
            //display wallets
            
            Console.WriteLine("The following are your wallets:");
            Console.WriteLine();
            foreach (var wallet in Storage.ActiveUser.Wallets)
            {
                Console.WriteLine(wallet.currency + " wallet");
            }
            Console.WriteLine();
            Console.WriteLine("type '<Currency>' to choose the corresponding wallet ");
            Console.WriteLine("type 'Create <Eur>' or 'Create <Rub>' to create a new wallet");
            var input = Console.ReadLine();

            switch (input.ToLower())
            {
                case "usd":
                    Storage.ActiveWallet = Storage.ActiveUser.Wallets.FirstOrDefault(u => u.currency == Currency.USD);
                    clearConsole();
                    walletMenu();
                    break;
                case "eur":
                    if (Storage.ActiveUser.Wallets.FirstOrDefault(u => u.currency == Currency.EUR) != null)
                    {
                        Storage.ActiveWallet = Storage.ActiveUser.Wallets.FirstOrDefault(u => u.currency == Currency.EUR);
                        clearConsole();
                        walletMenu();
                    }
                    else
                    {
                        clearConsole();
                        userMenu();
                    }
                    break;
                case "rub":
                    if (Storage.ActiveUser.Wallets.FirstOrDefault(u => u.currency == Currency.RUB) != null)
                    {
                        Storage.ActiveWallet = Storage.ActiveUser.Wallets.FirstOrDefault(u => u.currency == Currency.RUB);
                        clearConsole();
                        walletMenu();
                    }
                    else
                    {
                        clearConsole();
                        userMenu();
                    }
                    break;
                case "create eur":
                    if (Storage.ActiveUser.Wallets.FirstOrDefault(u => u.currency == Currency.EUR) != null)
                    {
                        Console.WriteLine("you already have a euro wallet");
                        Console.ReadLine();
                        clearConsole();
                        userMenu();
                    }
                    var wallet_eur = new Wallet(Currency.EUR);
                    Storage.ActiveUser.Wallets.Add(wallet_eur);
                    clearConsole();
                    userMenu();
                    break;
                case "create rub":
                    if (Storage.ActiveUser.Wallets.FirstOrDefault(u => u.currency == Currency.RUB) != null)
                    {
                        Console.WriteLine("you already have a ruble wallet");
                        Console.ReadLine();
                        clearConsole();
                        userMenu();
                    }
                    var wallet_rub = new Wallet(Currency.RUB);
                    Storage.ActiveUser.Wallets.Add(wallet_rub);
                    clearConsole();
                    userMenu();
                    break;
                default:
                    clearConsole();
                    userMenu();
                    break;
            }
        }

        private void walletMenu(DateTime? from = null, DateTime? to = null)
        {
            Console.WriteLine("This is your "+ Storage.ActiveWallet.currency.ToString() + " wallet:");
            Console.WriteLine("Your balance is " + Storage.ActiveWallet.balance);
            Storage.ActiveWallet.CollectStatistics(from, to);
            Console.WriteLine();

            Console.WriteLine("type 'Add income' to add income to wallet");
            Console.WriteLine("type 'Add expense' to add expense to wallet");

            var input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "add income":
                    clearConsole();
                    addIncomeMenu();
                    break;
                case "add expense":
                    clearConsole();
                    addExpenseMenu();
                    break;
                default:
                    break;
            }
        }
        private void addIncomeMenu() {
            Income income = new Income();
            bool check;
            string input;

            income.Date = DateTime.Now;
            Console.WriteLine("choose income type by typing it:");
            var incomeTypes = (IncomeType[])Enum.GetValues(typeof(IncomeType));
            foreach (IncomeType it in incomeTypes) {
                Console.WriteLine(it);
            }
            do
            {
                input = Console.ReadLine();
                check = Array.IndexOf(incomeTypes, input) != -1;
                if (check)
                {
                    income.IncomeType = (IncomeType)Enum.Parse(typeof(IncomeType), input);
                }
            } while (!check);

            Console.WriteLine("Enter the amount of income:");
            do
            {
                input = Console.ReadLine();
                check = SD.MoneyReg.IsMatch(input);
                if (check)
                {
                    income.amount = input;
                }
            }while(!check);

            Storage.ActiveWallet.AddOperation(income);
            

        }
        private void addExpenseMenu() { 
            Console.WriteLine("choose expense type:");

        }

        private void LoginMenu()
        {
            bool success = _authentication.LoginUser();
            if (!success)
            {
                clearConsole();
                MainMenu();
            }
            else
            {
                clearConsole();
                userMenu();
            }
        }

        private void RegisterMenu()
        {
            bool success = _authentication.RegisterUser();
            if(!success)
            {
                clearConsole();
                MainMenu();
            }
            else
            {
                clearConsole();
                LoginMenu();
            }
        }

        private void clearConsole()
        {
            Console.Clear();
        }
    }
}
