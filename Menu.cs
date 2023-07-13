using lab_2.NHibernate.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab_2
{
    public class Menu
    {
        UserAction userAction = new UserAction();
        public void start()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Menu selecting objects:");
                Console.WriteLine("1) Clients");
                Console.WriteLine("2) Cashiers");
                Console.WriteLine("3) Currencies");
                Console.WriteLine("4) Rates");
                Console.WriteLine("5) Transactions");
                Console.WriteLine("6) Exit");
                Console.WriteLine("Enter: ");

                int action = Convert.ToInt32(Console.ReadLine());

                switch (action)
                {
                    case 1:
                        Console.WriteLine();
                        menuClients();
                        break;
                    case 2:
                        Console.WriteLine();
                        menuCashiers();
                        break;
                    case 3:
                        Console.WriteLine();
                        menuCurrencies();
                        break;
                    case 4:
                        Console.WriteLine();
                        menuRates();
                        break;
                    case 5:
                        Console.WriteLine();
                        menuTransactions();
                        break;
                    case 6:
                        exit = true;
                        break;
                }
            }
        }

        private void menuClients()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Menu clients:");
                Console.WriteLine("1) View");
                Console.WriteLine("2) Insert");
                Console.WriteLine("3) Delete");
                Console.WriteLine("4) Update");
                Console.WriteLine("5) Search");
                Console.WriteLine("6) Exit");
                Console.WriteLine("Enter: ");

                int action = Convert.ToInt32(Console.ReadLine());

                switch (action)
                {
                    case 1:
                        Console.WriteLine();
                        userAction.viewClients();
                        break;
                    case 2:
                        Console.WriteLine();
                        userAction.insertClients();
                        break;
                    case 3:
                        Console.WriteLine();
                        userAction.deleteClients();
                        break;
                    case 4:
                        Console.WriteLine();
                        userAction.updateClients();
                        break;
                    case 5:
                        Console.WriteLine();
                        userAction.searchClients();
                        break;
                    case 6:

                        exit = true;
                        break;
                }
            }
        }

        private void menuCashiers()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Menu cashiers:");
                Console.WriteLine("1) View");
                Console.WriteLine("2) Insert");
                Console.WriteLine("3) Delete");
                Console.WriteLine("4) Update");
                Console.WriteLine("5) Search");
                Console.WriteLine("6) Exit");
                Console.WriteLine("Enter: ");

                int action = Convert.ToInt32(Console.ReadLine());

                switch (action)
                {
                    case 1:
                        Console.WriteLine();
                        userAction.viewCashiers();
                        break;
                    case 2:
                        Console.WriteLine();
                        userAction.insertCashiers();
                        break;
                    case 3:
                        Console.WriteLine();
                        userAction.deleteCashiers();
                        break;
                    case 4:
                        Console.WriteLine();
                        userAction.updateCashiers();
                        break;
                    case 5:
                        Console.WriteLine();
                        userAction.searchCashiers();
                        break;
                    case 6:

                        exit = true;
                        break;
                }
            }
        }

        private void menuCurrencies()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Menu currencies:");
                Console.WriteLine("1) View");
                Console.WriteLine("2) Insert");
                Console.WriteLine("3) Delete");
                Console.WriteLine("4) Update");
                Console.WriteLine("5) Search");
                Console.WriteLine("6) Exit");
                Console.WriteLine("Enter: ");

                int action = Convert.ToInt32(Console.ReadLine());

                switch (action)
                {
                    case 1:
                        Console.WriteLine();
                        userAction.viewCurrencies();
                        break;
                    case 2:
                        Console.WriteLine();
                        userAction.insertCurrencies();
                        break;
                    case 3:
                        Console.WriteLine();
                        userAction.deleteCurrencies();
                        break;
                    case 4:
                        Console.WriteLine();
                        userAction.updateCurrencies();
                        break;
                    case 5:
                        Console.WriteLine();
                        userAction.searchCurrencies();
                        break;
                    case 6:

                        exit = true;
                        break;
                }
            }
        }

        private void menuRates()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Menu rates:");
                Console.WriteLine("1) View");
                Console.WriteLine("2) Insert");
                Console.WriteLine("3) Delete");
                Console.WriteLine("4) Update");
                Console.WriteLine("5) Search");
                Console.WriteLine("6) Exit");
                Console.WriteLine("Enter: ");

                int action = Convert.ToInt32(Console.ReadLine());

                switch (action)
                {
                    case 1:
                        Console.WriteLine();
                        userAction.viewRates();
                        break;
                    case 2:
                        Console.WriteLine();
                        userAction.insertRates();
                        break;
                    case 3:
                        Console.WriteLine();
                        userAction.deleteRates();
                        break;
                    case 4:
                        Console.WriteLine();
                        userAction.updateRates();
                        break;
                    case 5:
                        Console.WriteLine();
                        userAction.searchRates();
                        break;
                    case 6:

                        exit = true;
                        break;
                }
            }
        }

        private void menuTransactions()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Menu transactions:");
                Console.WriteLine("1) View");
                Console.WriteLine("2) Insert");
                Console.WriteLine("3) Delete");
                Console.WriteLine("4) Update");
                Console.WriteLine("5) Search");
                Console.WriteLine("6) Exit");
                Console.WriteLine("Enter: ");

                int action = Convert.ToInt32(Console.ReadLine());

                switch (action)
                {
                    case 1:
                        Console.WriteLine();
                        userAction.viewTransactions();
                        break;
                    case 2:
                        Console.WriteLine();
                        userAction.insertTransactions();
                        break;
                    case 3:
                        Console.WriteLine();
                        userAction.deleteTransactions();
                        break;
                    case 4:
                        Console.WriteLine();
                        userAction.updateTransactions();
                        break;
                    case 5:
                        Console.WriteLine();
                        userAction.searchTransactions();
                        break;
                    case 6:

                        exit = true;
                        break;
                }
            }
        }
    }
}
