using fintech.Utility;
using Microsoft.Win32.SafeHandles;
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
            Console.WriteLine("type 'Delete <Eur/Usd/Rub>' to delete a wallet");
            Console.WriteLine("type 'logout' to logout");
            var input = Console.ReadLine();

            switch (input.ToLower())
            {
                case "logout":
                    _authentication.Logout();
                    clearConsole();
                    MainMenu();
                    break;
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
                case "create usd":
                    if (Storage.ActiveUser.Wallets.FirstOrDefault(u => u.currency == Currency.USD) != null)
                    {
                        Console.WriteLine("you already have a usd wallet");
                        Console.ReadLine();
                        clearConsole();
                        userMenu();
                    }
                    var wallet_usd = new Wallet(Currency.USD);
                    Storage.ActiveUser.Wallets.Add(wallet_usd);
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
                case "delete usd":
                    var walletusd = Storage.ActiveUser.Wallets.FirstOrDefault(u=>u.currency== Currency.USD);
                    if (walletusd != null)
                    {
                        Storage.ActiveUser.Wallets.Remove(walletusd);
                        clearConsole();
                        userMenu();
                    }
                    else
                    {
                        clearConsole();
                        userMenu();
                    }
                    break;
                case "delete eur":
                    var walleteur = Storage.ActiveUser.Wallets.FirstOrDefault(u => u.currency == Currency.EUR);
                    if (walleteur != null)
                    {
                        Storage.ActiveUser.Wallets.Remove(walleteur);
                        clearConsole();
                        userMenu();
                    }
                    else
                    {
                        clearConsole();
                        userMenu();
                    }
                    break;
                case "delete rub":
                    var walletrub = Storage.ActiveUser.Wallets.FirstOrDefault(u => u.currency == Currency.RUB);
                    if (walletrub != null)
                    {
                        Storage.ActiveUser.Wallets.Remove(walletrub);
                        clearConsole();
                        userMenu();
                    }
                    else
                    {
                        clearConsole();
                        userMenu();
                    }
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
            Console.WriteLine();

            Console.WriteLine("type 'Add income' to add income to wallet");
            Console.WriteLine("type 'Add expense' to add expense to wallet");
            Console.WriteLine("type 'V' to view statistics");
            Console.WriteLine("type 'logout' to logout");
            Console.WriteLine("type 'usermenu' to go back to user menu");

            var input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "usermenu":
                    clearConsole();
                    userMenu();
                    break;
                case "logout":
                    _authentication.Logout();
                    clearConsole();
                    MainMenu();
                    break;
                case "v":
                    clearConsole();
                    statisticsMenu();
                    break;
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

            Console.WriteLine("Enter a date in the format mm/dd/yyyy");
            do
            {
                input = Console.ReadLine();
                check = SD.datereg.IsMatch(input);
                if (check)
                {
                    try
                    {
                        income.Date = DateTime.Parse(input);
                    } catch(Exception ex)
                    {
                        check = false;
                    }
                }
            } while (!check);


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
            Console.WriteLine("Enter a date in the format mm/dd/yyyy");
            do
            {
                input = Console.ReadLine();
                check = SD.datereg.IsMatch(input);
                if (check)
                {
                    try
                    {
                        expense.Date = DateTime.Parse(input);
                    }
                    catch (Exception ex)
                    {
                        check = false;
                    }
                }
            } while (!check);

            Storage.ActiveWallet.AddOperation(expense);
            Storage.UpdateLists();
            walletMenu();
        }
        private void statisticsMenu()
        {
            Storage.ActiveWallet.CollectStatistics(new DateTime(year:2000,01,01), DateTime.Now);
            Console.WriteLine("Type c to choose a range of dates");
            Console.WriteLine("Type 'logout' to logout");
            Console.WriteLine("Type 'usemenu' to go back to user menu");
            Console.WriteLine("Type 'walletmenu' to go back to wallet menu");

            var input = Console.ReadLine();
            bool check;
            DateTime from = DateTime.Now;
            DateTime to = DateTime.Now;

            switch(input.ToLower())
            {
                case "logout":
                    _authentication.Logout();
                    clearConsole();
                    MainMenu();
                    break;
                case "usermenu":
                    clearConsole();
                    userMenu();
                    break;
                case "walletmenu":
                    clearConsole();
                    walletMenu();
                    break;
                case "c":
                    Console.WriteLine("enter date 'from' in the format mm/dd/yyyy:");
                    do
                    {
                        input = Console.ReadLine();
                        check = SD.datereg.IsMatch(input);
                        if (check)
                        {
                            try
                            {
                                from = DateTime.Parse(input);
                            }
                            catch (Exception ex)
                            {
                                check = false;
                            }
                        }
                    } while (!check);

                    Console.WriteLine("enter date 'to' in the format mm/dd/yyyy:");
                    do
                    {
                        input = Console.ReadLine();
                        check = SD.datereg.IsMatch(input);
                        if (check)
                        {
                            try
                            {
                                to = DateTime.Parse(input);
                            }
                            catch (Exception ex)
                            {
                                check = false;
                            }
                        }
                    } while (!check);
                    break;
                default:
                    statisticsMenu();
                    break;
            }
            clearConsole();
            Storage.ActiveWallet.CollectStatistics(from, to);
            Console.WriteLine("type 'logout' to logout");
            Console.WriteLine("type 'usermenu' to go back to user menu");
            input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "usermenu":
                    clearConsole();
                    userMenu();
                    break;
                case "logout":
                    _authentication.Logout();
                    clearConsole();
                    MainMenu();
                    break;
                default:
                    clearConsole();
                    walletMenu();
                    break;
            }


        }
        private void LoginMenu()
        {
            bool success = _authentication.LoginUser();
            if (!success)
            {
                clearConsole();
                Console.WriteLine("your login credentials were wrong");
                Console.WriteLine();
                Console.WriteLine("type m to main menu");
                Console.WriteLine("press any key to login again");
                var input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "m":
                        clearConsole();
                        MainMenu();
                        break;
                    default:
                        clearConsole();
                        LoginMenu();
                        break;
                }
                
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
