using fintech.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
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
            Console.WriteLine("2- Login");

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

        private void walletMenu()
        {
            Console.WriteLine("This is your "+ Storage.ActiveWallet.currency.ToString() + " wallet:");
            Console.WriteLine("Your balance is " + Storage.ActiveWallet.balance.DisplayNumAsStr());
            Storage.ActiveWallet.CollectStatistics();
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
                    clearConsole();
                    walletMenu();
                    break;
            }
        }
        private void addIncomeMenu() {
            Income income = new Income();
            bool check;
            string input;

            Console.WriteLine("choose income type by typing it:");
            var incomeTypes = (IncomeType[])Enum.GetValues(typeof(IncomeType));
            foreach (IncomeType it in incomeTypes) {
                Console.WriteLine(it);
            }
            do
            {
                input = Console.ReadLine().ToLower();
                //check = Array.IndexOf(incomeTypes, input) != -1;
                check = Array.Exists(incomeTypes, u=>u.ToString().ToLower().Equals(input));
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
                    income.amount = new Money(input);
                }
            }while(!check);

            Storage.ActiveWallet.AddOperation(income);
            Storage.UpdateLists();
            walletMenu();

        }
        private void addExpenseMenu() {
            Expense expense = new Expense();
            bool check;
            string input;

            expense.Date = DateTime.Now;
            Console.WriteLine("choose expense type by typing it:");
            var expenseTypes = (ExpenseType[])Enum.GetValues(typeof(ExpenseType));
            foreach (ExpenseType it in expenseTypes)
            {
                Console.WriteLine(it);
            }
            do
            {
                input = Console.ReadLine().ToLower();
                check = Array.Exists(expenseTypes, u => u.ToString().ToLower().Equals(input));
                if (check)
                {
                    expense.ExpenseType = (ExpenseType)Enum.Parse(typeof(ExpenseType), input);
                }
            } while (!check);

            Console.WriteLine("Enter the amount of income:");
            do
            {
                input = Console.ReadLine();
                check = SD.MoneyReg.IsMatch(input);
                if (check)
                {
                    expense.amount = new Money(input);
                }
            } while (!check);

            Storage.ActiveWallet.AddOperation(expense);
            Storage.UpdateLists();
            walletMenu();
        }

        private void LoginMenu()
        {
            bool success = _authentication.LoginUser();
            if (!success)
            {
                clearConsole();
                LoginMenu();
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
                RegisterMenu();
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
