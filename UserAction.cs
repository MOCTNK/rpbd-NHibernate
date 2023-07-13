using lab_2.NHibernate;
using lab_2.NHibernate.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab_2
{
    public class UserAction
    {
        private int countObjectOnPage = 5;
        
        public void viewClients()
        {
            int pageClients = 1;
            int rows = HQLHelper.getClientsCountRows();
            if(rows > 0)
            {
                int id1 = 1 + (pageClients - 1) * countObjectOnPage;
                int id2 = countObjectOnPage + (pageClients - 1) * countObjectOnPage;
                using (var session = NHibernateHelper.OpenSession())
                {
                    bool exit = false;
                    while (!exit)
                    {
                        var clients = HQLHelper.getListInRange(session, "FROM Clients ORDER BY Id", id1, id2).List();
                        Console.WriteLine("{0}{1}{1}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                        Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}| {3, -20}| {4, -20}| {5, -20}|", "N", "name", "surname", "patronymic", "passport_series", "passport_number");
                        Console.WriteLine("{0}{1}{1}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                        int i = id1;
                        foreach (Clients client in clients)
                        {
                            Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}| {3, -20}| {4, -20}| {5, -20}|", i, client.Name, client.Surname, client.Patronymic, client.Passport_series, client.Passport_number);
                            Console.WriteLine("{0}{1}{1}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                            i++;
                        }

                        int maxPage = rows % countObjectOnPage == 0 ? rows / countObjectOnPage : rows / countObjectOnPage + 1;
                        Console.WriteLine("pages: " + pageClients + "/" + maxPage);
                        Console.WriteLine("1) next");
                        Console.WriteLine("2) prev");
                        Console.WriteLine("3) Exit");
                        Console.WriteLine("Enter action: ");
                        int action = Convert.ToInt32(Console.ReadLine());

                        switch (action)
                        {
                            case 1:
                                {
                                    if (pageClients < maxPage)
                                    {
                                        id1 += countObjectOnPage;
                                        id2 += countObjectOnPage;
                                        pageClients++;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if (pageClients > 1)
                                    {
                                        id1 -= countObjectOnPage;
                                        id2 -= countObjectOnPage;
                                        pageClients--;
                                    }
                                }
                                break;
                            case 3:
                                exit = true;
                                break;
                        }
                    }
                }
            } else
            {
                Console.WriteLine("Object is empty!");
            }
        }

        public void insertClients()
        {
            int rows = HQLHelper.getClientsCountRows();
            string name;
            while (true)
            {
                Console.WriteLine("Enter name: ");
                name = Convert.ToString(Console.ReadLine());
                if (name.Length > 0 && name.Length <= 50 && isOnlyString(name))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            string surname;
            while (true)
            {
                Console.WriteLine("Enter surname: ");
                surname = Convert.ToString(Console.ReadLine());
                if (surname.Length > 0 && surname.Length <= 50 && isOnlyString(surname))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string patronymic;
            while (true)
            {
                Console.WriteLine("Enter patronymic: ");
                patronymic = Convert.ToString(Console.ReadLine());
                if (patronymic.Length > 0 && patronymic.Length <= 50 && isOnlyString(patronymic))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            string passportSeries;
            while (true)
            {
                Console.WriteLine("Enter passport series: ");
                passportSeries = Convert.ToString(Console.ReadLine());
                if (passportSeries.Length == 4 && isOnlyInt(passportSeries))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            string passportNumber;
            while (true)
            {
                Console.WriteLine("Enter passport number: ");
                passportNumber = Convert.ToString(Console.ReadLine());
                if (passportNumber.Length == 6 && isOnlyInt(passportNumber))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    Clients newClients = new Clients();
                    newClients.Name = name;
                    newClients.Surname = surname;
                    newClients.Patronymic = patronymic;
                    newClients.Passport_series = passportSeries;
                    newClients.Passport_number = passportNumber;
                    session.Save(newClients);
                    transaction.Commit();
                }
            }
            Console.WriteLine("Node inserted!");
        }

        public void deleteClients()
        {
            int rows = HQLHelper.getClientsCountRows();
            string id;
            while (true)
            {
                Console.WriteLine("Enter N nodes: ");
                id = Convert.ToString(Console.ReadLine());
                if (isOnlyInt(id) && Convert.ToInt32(id) > 0 && Convert.ToInt32(id) <= rows)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var clients = HQLHelper.getObjectByN(session, "FROM Clients ORDER BY Id", Convert.ToInt32(id)).List();
                    foreach(Clients client in clients)
                    {
                        session.Delete(client);
                    }
                    transaction.Commit();
                }
            }
            Console.WriteLine("Node deleted!");
        }

        public void updateClients()
        {
            string id;
            int rows = HQLHelper.getClientsCountRows();
            while (true)
            {
                Console.WriteLine("Enter N nodes: ");
                id = Convert.ToString(Console.ReadLine());
                if (isOnlyInt(id) && Convert.ToInt32(id) > 0 && Convert.ToInt32(id) <= rows)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            int column;
            while (true)
            {
                Console.WriteLine("1)name");
                Console.WriteLine("2)surname");
                Console.WriteLine("3)patronymic");
                Console.WriteLine("4)passport_series");
                Console.WriteLine("5)passport_number");
                Console.WriteLine("Enter column: ");
                column = Convert.ToInt32(Console.ReadLine());
                if (column >= 1 && column <= 5)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string data;
            while (true)
            {
                Console.WriteLine("Enter data: ");
                data = Convert.ToString(Console.ReadLine());

                if (column >= 1 && column <= 3 && isOnlyString(data) && data.Length > 0 && data.Length <= 50)
                {
                    break;
                }
                if (column == 4 && isOnlyInt(data) && data.Length == 4)
                {
                    break;
                }
                if (column == 5 && isOnlyInt(data) && data.Length == 6)
                {
                    break;
                }
                Console.WriteLine("Incorrect value!");
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var clients = HQLHelper.getObjectByN(session, "FROM Clients ORDER BY Id", Convert.ToInt32(id)).List();
                    foreach (Clients client in clients)
                    {
                        switch (column)
                        {
                            case 1:
                                client.Name = data;
                                break;
                            case 2:
                                client.Surname = data;
                                break;
                            case 3:
                                client.Patronymic = data;
                                break;
                            case 4:
                                client.Passport_series = data;
                                break;
                            case 5:
                                client.Passport_number = data;
                                break;
                        }
                        session.SaveOrUpdate(client);
                    }
                    transaction.Commit();
                }
            }
            Console.WriteLine("Node updated!");
        }

        public void searchClients()
        {

            string data;
            Console.WriteLine("Enter data: ");
            data = Convert.ToString(Console.ReadLine());

            using (var session = NHibernateHelper.OpenSession())
            {
                var clients = HQLHelper.searchClients(session, data).List();
                if(clients.Count > 0)
                {
                    Console.WriteLine("{0}{1}{1}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                    Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}| {3, -20}| {4, -20}| {5, -20}|", "N", "name", "surname", "patronymic", "passport_series", "passport_number");
                    Console.WriteLine("{0}{1}{1}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                    int i = 1;
                    foreach (Clients client in clients)
                    {
                        Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}| {3, -20}| {4, -20}| {5, -20}|", i, client.Name, client.Surname, client.Patronymic, client.Passport_series, client.Passport_number);
                        Console.WriteLine("{0}{1}{1}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                        i++;
                    }
                } else
                {
                    Console.WriteLine("Nothing found!");
                }
            }
        }

        public void viewCashiers()
        {
            int pageCashiers = 1;
            int rows = HQLHelper.getCashiersCountRows();
            if(rows > 0)
            {
                int id1 = 1 + (pageCashiers - 1) * countObjectOnPage;
                int id2 = countObjectOnPage + (pageCashiers - 1) * countObjectOnPage;
                using (var session = NHibernateHelper.OpenSession())
                {
                    bool exit = false;
                    while (!exit)
                    {
                        var cashiers = HQLHelper.getListInRange(session, "FROM Cashiers ORDER BY Id", id1, id2).List();
                        Console.WriteLine("{0}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                        Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}| {3, -20}|", "N", "name", "surname", "patronymic");
                        Console.WriteLine("{0}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                        int i = id1;
                        foreach (Cashiers cashier in cashiers)
                        {
                            Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}| {3, -20}|", i, cashier.Name, cashier.Surname, cashier.Patronymic);
                            Console.WriteLine("{0}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                            i++;
                        }

                        int maxPage = rows % countObjectOnPage == 0 ? rows / countObjectOnPage : rows / countObjectOnPage + 1;
                        Console.WriteLine("pages: " + pageCashiers + "/" + maxPage);
                        Console.WriteLine("1) next");
                        Console.WriteLine("2) prev");
                        Console.WriteLine("3) Exit");
                        Console.WriteLine("Enter action: ");
                        int action = Convert.ToInt32(Console.ReadLine());

                        switch (action)
                        {
                            case 1:
                                {
                                    if (pageCashiers < maxPage)
                                    {
                                        id1 += countObjectOnPage;
                                        id2 += countObjectOnPage;
                                        pageCashiers++;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if (pageCashiers > 1)
                                    {
                                        id1 -= countObjectOnPage;
                                        id2 -= countObjectOnPage;
                                        pageCashiers--;
                                    }
                                }
                                break;
                            case 3:
                                exit = true;
                                break;
                        }
                    }
                }
            } else
            {
                Console.WriteLine("Object is empty!");
            }
        }

        public void insertCashiers()
        {
            int rows = HQLHelper.getCashiersCountRows();
            string name;
            while (true)
            {
                Console.WriteLine("Enter name: ");
                name = Convert.ToString(Console.ReadLine());
                if (name.Length > 0 && name.Length <= 50 && isOnlyString(name))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            string surname;
            while (true)
            {
                Console.WriteLine("Enter surname: ");
                surname = Convert.ToString(Console.ReadLine());
                if (surname.Length > 0 && surname.Length <= 50 && isOnlyString(surname))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string patronymic;
            while (true)
            {
                Console.WriteLine("Enter patronymic: ");
                patronymic = Convert.ToString(Console.ReadLine());
                if (patronymic.Length > 0 && patronymic.Length <= 50 && isOnlyString(patronymic))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    Cashiers newCashiers = new Cashiers();
                    newCashiers.Name = name;
                    newCashiers.Surname = surname;
                    newCashiers.Patronymic = patronymic;
                    session.Save(newCashiers);
                    transaction.Commit();
                }
            }
            Console.WriteLine("Node inserted!");
        }

        public void deleteCashiers()
        {
            int rows = HQLHelper.getCashiersCountRows();
            string id;
            while (true)
            {
                Console.WriteLine("Enter N nodes: ");
                id = Convert.ToString(Console.ReadLine());
                if (isOnlyInt(id) && Convert.ToInt32(id) > 0 &&  Convert.ToInt32(id) <= rows)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var cashiers = HQLHelper.getObjectByN(session, "FROM Cashiers ORDER BY Id", Convert.ToInt32(id)).List();
                    foreach (Cashiers cashier in cashiers)
                    {
                        session.Delete(cashier);
                    }
                    transaction.Commit();
                }
            }
            Console.WriteLine("Node deleted!");
        }

        public void updateCashiers()
        {
            string id;
            int rows = HQLHelper.getCashiersCountRows();
            while (true)
            {
                Console.WriteLine("Enter N nodes: ");
                id = Convert.ToString(Console.ReadLine());
                if (isOnlyInt(id) && Convert.ToInt32(id) > 0 && Convert.ToInt32(id) <= rows)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            int column;
            while (true)
            {
                Console.WriteLine("1)name");
                Console.WriteLine("2)surname");
                Console.WriteLine("3)patronymic");
                Console.WriteLine("Enter column: ");
                column = Convert.ToInt32(Console.ReadLine());
                if (column >= 1 && column <= 3)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string data;
            while (true)
            {
                Console.WriteLine("Enter data: ");
                data = Convert.ToString(Console.ReadLine());

                if (column >= 1 && column <= 3 && isOnlyString(data) && data.Length > 0 && data.Length <= 50)
                {
                    break;
                }
                Console.WriteLine("Incorrect value!");
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var cashiers = HQLHelper.getObjectByN(session, "FROM Cashiers ORDER BY Id", Convert.ToInt32(id)).List();
                    foreach (Cashiers cashier in cashiers)
                    {
                        switch (column)
                        {
                            case 1:
                                cashier.Name = data;
                                break;
                            case 2:
                                cashier.Surname = data;
                                break;
                            case 3:
                                cashier.Patronymic = data;
                                break;
                        }
                        session.SaveOrUpdate(cashier);
                    }
                    transaction.Commit();
                }
            }
            Console.WriteLine("Node updated!");
        }

        public void searchCashiers()
        {

            string data;
            Console.WriteLine("Enter data: ");
            data = Convert.ToString(Console.ReadLine());

            using (var session = NHibernateHelper.OpenSession())
            {
                var cashiers = HQLHelper.searchCashiers(session, data).List();
                if(cashiers.Count > 0)
                {
                    Console.WriteLine("{0}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                    Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}| {3, -20}|", "N", "name", "surname", "patronymic");
                    Console.WriteLine("{0}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                    int i = 1;
                    foreach (Cashiers cashier in cashiers)
                    {
                        Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}| {3, -20}|", i, cashier.Name, cashier.Surname, cashier.Patronymic);
                        Console.WriteLine("{0}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                        i++;
                    }
                } else
                {
                    Console.WriteLine("Nothing found!");
                }
            }
        }

        public void viewCurrencies()
        {
            int pageCurrencies = 1;
            int rows = HQLHelper.getCurrenciesCountRows();
            if(rows > 0)
            {
                int id1 = 1 + (pageCurrencies - 1) * countObjectOnPage;
                int id2 = countObjectOnPage + (pageCurrencies - 1) * countObjectOnPage;
                using (var session = NHibernateHelper.OpenSession())
                {
                    bool exit = false;
                    while (!exit)
                    {
                        var currencies = HQLHelper.getListInRange(session, "FROM Currencies ORDER BY Id", id1, id2).List();
                        Console.WriteLine("{0}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                        Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}|", "N", "code", "name");
                        Console.WriteLine("{0}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                        int i = id1;
                        foreach (Currencies currency in currencies)
                        {
                            Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}|", i, currency.Code, currency.Name);
                            Console.WriteLine("{0}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                            i++;
                        }

                        int maxPage = rows % countObjectOnPage == 0 ? rows / countObjectOnPage : rows / countObjectOnPage + 1;
                        Console.WriteLine("pages: " + pageCurrencies + "/" + maxPage);
                        Console.WriteLine("1) next");
                        Console.WriteLine("2) prev");
                        Console.WriteLine("3) Exit");
                        Console.WriteLine("Enter action: ");
                        int action = Convert.ToInt32(Console.ReadLine());

                        switch (action)
                        {
                            case 1:
                                {
                                    if (pageCurrencies < maxPage)
                                    {
                                        id1 += countObjectOnPage;
                                        id2 += countObjectOnPage;
                                        pageCurrencies++;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if (pageCurrencies > 1)
                                    {
                                        id1 -= countObjectOnPage;
                                        id2 -= countObjectOnPage;
                                        pageCurrencies--;
                                    }
                                }
                                break;
                            case 3:
                                exit = true;
                                break;
                        }
                    }
                }
            } else
            {
                Console.WriteLine("Object is empty!");
            }
        }

        public void insertCurrencies()
        {
            int rows = HQLHelper.getCurrenciesCountRows();
            string code;
            while (true)
            {
                Console.WriteLine("Enter code: ");
                code = Convert.ToString(Console.ReadLine());
                if (code.Length > 0 && code.Length <= 3 && isOnlyInt(code))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string name;
            while (true)
            {
                Console.WriteLine("Enter name: ");
                name = Convert.ToString(Console.ReadLine());
                if (name.Length > 0 && name.Length <= 3 && isOnlyString(name))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    Currencies newCurrencies = new Currencies();
                    newCurrencies.Code = code;
                    newCurrencies.Name = name;
                    session.Save(newCurrencies);
                    transaction.Commit();
                }
            }
            Console.WriteLine("Node inserted!");
        }

        public void deleteCurrencies()
        {
            int rows = HQLHelper.getCurrenciesCountRows();
            string id;
            while (true)
            {
                Console.WriteLine("Enter N nodes: ");
                id = Convert.ToString(Console.ReadLine());
                if (isOnlyInt(id) && Convert.ToInt32(id) <= rows)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var currencies = HQLHelper.getObjectByN(session, "FROM Currencies ORDER BY Id", Convert.ToInt32(id)).List();
                    foreach (Currencies currency in currencies)
                    {
                        session.Delete(currency);
                    }
                    transaction.Commit();
                }
            }
            Console.WriteLine("Node deleted!");
        }

        public void updateCurrencies()
        {
            string id;
            int rows = HQLHelper.getCurrenciesCountRows();
            while (true)
            {
                Console.WriteLine("Enter N nodes: ");
                id = Convert.ToString(Console.ReadLine());
                if (isOnlyInt(id) && Convert.ToInt32(id) > 0 && Convert.ToInt32(id) <= rows)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            int column;
            while (true)
            {
                Console.WriteLine("1)code");
                Console.WriteLine("2)name");
                Console.WriteLine("Enter column: ");
                column = Convert.ToInt32(Console.ReadLine());
                if (column >= 1 && column <= 2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string data;
            while (true)
            {
                Console.WriteLine("Enter data: ");
                data = Convert.ToString(Console.ReadLine());

                if (column == 1 && isOnlyInt(data) && data.Length == 3)
                {
                    break;
                }
                if (column == 2 && isOnlyString(data) && data.Length > 0 && data.Length <= 3)
                {
                    break;
                }
                Console.WriteLine("Incorrect value!");
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var currencies = HQLHelper.getObjectByN(session, "FROM Currencies ORDER BY Id", Convert.ToInt32(id)).List();
                    foreach (Currencies currency in currencies)
                    {
                        switch (column)
                        {
                            case 1:
                                currency.Code = data;
                                break;
                            case 2:
                                currency.Name = data;
                                break;
                        }
                        session.SaveOrUpdate(currency);
                    }
                    transaction.Commit();
                }
            }
            Console.WriteLine("Node updated!");
        }

        public void searchCurrencies()
        {

            string data;
            Console.WriteLine("Enter data: ");
            data = Convert.ToString(Console.ReadLine());

            using (var session = NHibernateHelper.OpenSession())
            {
                var currencies = HQLHelper.searchCurrencies(session, data).List();
                if(currencies.Count > 0)
                {
                    Console.WriteLine("{0}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                    Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}|", "N", "code", "name");
                    Console.WriteLine("{0}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                    int i = 1;
                    foreach (Currencies currency in currencies)
                    {
                        Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}|", i, currency.Code, currency.Name);
                        Console.WriteLine("{0}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                        i++;
                    }
                } else
                {
                    Console.WriteLine("Nothing found!");
                }
            }
        }

        public void viewRates()
        {
            int pageRates = 1;
            int rows = HQLHelper.getRatesCountRows();
            if(rows > 0)
            {
                int id1 = 1 + (pageRates - 1) * countObjectOnPage;
                int id2 = countObjectOnPage + (pageRates - 1) * countObjectOnPage;
                using (var session = NHibernateHelper.OpenSession())
                {
                    bool exit = false;
                    while (!exit)
                    {
                        var rates = HQLHelper.getListInRange(session, "FROM Rates ORDER BY Id", id1, id2).List();
                        Console.WriteLine("{0}{1}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                        Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}| {3, -20}| {4, -20}|", "N", "sold", "purchased", "sale_rate", "purchase_rate");
                        Console.WriteLine("{0}{1}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                        int i = id1;
                        foreach (Rates rate in rates)
                        {
                            Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}| {3, -20}| {4, -20}|", i, rate.IdCurrencySold.Name, rate.IdCurrencyPurchased.Name, rate.SaleRate, rate.PurchaseRate);
                            Console.WriteLine("{0}{1}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                            i++;
                        }

                        int maxPage = rows % countObjectOnPage == 0 ? rows / countObjectOnPage : rows / countObjectOnPage + 1;
                        Console.WriteLine("pages: " + pageRates + "/" + maxPage);
                        Console.WriteLine("1) next");
                        Console.WriteLine("2) prev");
                        Console.WriteLine("3) Exit");
                        Console.WriteLine("Enter action: ");
                        int action = Convert.ToInt32(Console.ReadLine());

                        switch (action)
                        {
                            case 1:
                                {
                                    if (pageRates < maxPage)
                                    {
                                        id1 += countObjectOnPage;
                                        id2 += countObjectOnPage;
                                        pageRates++;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if (pageRates > 1)
                                    {
                                        id1 -= countObjectOnPage;
                                        id2 -= countObjectOnPage;
                                        pageRates--;
                                    }
                                }
                                break;
                            case 3:
                                exit = true;
                                break;
                        }
                    }
                }
            } else
            {
                Console.WriteLine("Object is empty!");
            }
        }

        public void insertRates()
        {
            int rows = HQLHelper.getRatesCountRows();
            int rowsCurrencies = HQLHelper.getCurrenciesCountRows();

            string N_currency_sold;
            while (true)
            {
                Console.WriteLine("Enter N currencies sold: ");
                N_currency_sold = Convert.ToString(Console.ReadLine());
                if (Convert.ToInt32(N_currency_sold) > 0 && Convert.ToInt32(N_currency_sold) <= rowsCurrencies && isOnlyInt(N_currency_sold))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string N_currency_purchased;
            while (true)
            {
                Console.WriteLine("Enter N currencies purchased: ");
                N_currency_purchased = Convert.ToString(Console.ReadLine());
                if (Convert.ToInt32(N_currency_purchased) != Convert.ToInt32(N_currency_sold) && Convert.ToInt32(N_currency_purchased) > 0 && Convert.ToInt32(N_currency_purchased) <= rowsCurrencies && isOnlyInt(N_currency_purchased))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string sale_rate;
            while (true)
            {
                Console.WriteLine("Enter sale rate: ");
                sale_rate = Convert.ToString(Console.ReadLine());
                if (sale_rate.Length > 0 && sale_rate.Length <= 10 && isOnlyIntAndPoint(sale_rate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string purchase_rate;
            while (true)
            {
                Console.WriteLine("Enter purchase rate: ");
                purchase_rate = Convert.ToString(Console.ReadLine());
                if (purchase_rate.Length > 0 && purchase_rate.Length <= 10 && isOnlyIntAndPoint(purchase_rate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    Rates newRates = new Rates();
                    var currenciesSold = HQLHelper.getObjectByN(session, "FROM Currencies ORDER BY Id", Convert.ToInt32(N_currency_sold)).List();
                    foreach (Currencies currency in currenciesSold)
                    {
                        newRates.IdCurrencySold = currency;
                    }
                    var currenciesPurchased = HQLHelper.getObjectByN(session, "FROM Currencies ORDER BY Id", Convert.ToInt32(N_currency_purchased)).List();
                    foreach (Currencies currency in currenciesPurchased)
                    {
                        newRates.IdCurrencyPurchased = currency;
                    }
                    newRates.SaleRate = double.Parse(sale_rate, System.Globalization.CultureInfo.InvariantCulture);
                    newRates.PurchaseRate = double.Parse(purchase_rate, System.Globalization.CultureInfo.InvariantCulture);
                    session.Save(newRates);
                    transaction.Commit();
                }
            }
            Console.WriteLine("Node inserted!");
        }

        public void deleteRates()
        {
            int rows = HQLHelper.getRatesCountRows();
            string id;
            while (true)
            {
                Console.WriteLine("Enter N nodes: ");
                id = Convert.ToString(Console.ReadLine());
                if (isOnlyInt(id) && Convert.ToInt32(id) > 0 && Convert.ToInt32(id) <= rows)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var rates = HQLHelper.getObjectByN(session, "FROM Rates ORDER BY Id", Convert.ToInt32(id)).List();
                    foreach (Rates rate in rates)
                    {
                        session.Delete(rate);
                    }
                    transaction.Commit();
                }
            }
            Console.WriteLine("Node deleted!");
        }

        public void updateRates()
        {
            string id;
            int rows = HQLHelper.getRatesCountRows();
            int rowsCurrencies = HQLHelper.getCurrenciesCountRows();
            while (true)
            {
                Console.WriteLine("Enter N nodes: ");
                id = Convert.ToString(Console.ReadLine());
                if (isOnlyInt(id) && Convert.ToInt32(id) > 0 &&  Convert.ToInt32(id) <= rows)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            int column;
            while (true)
            {
                Console.WriteLine("1)sold");
                Console.WriteLine("2)purchased");
                Console.WriteLine("3)sale_rate");
                Console.WriteLine("4)purchase_rate");
                Console.WriteLine("Enter column: ");
                column = Convert.ToInt32(Console.ReadLine());
                if (column >= 1 && column <= 4)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string data;
            while (true)
            {

                if (column == 1)
                {
                    Console.WriteLine("Enter N currencies sold: ");
                    data = Convert.ToString(Console.ReadLine());
                    if (isOnlyInt(data) && Convert.ToInt32(data) > 0 && Convert.ToInt32(data) <= rowsCurrencies)
                    {
                        break;
                    }
                }

                if (column == 2)
                {
                    Console.WriteLine("Enter N currencies purchased: ");
                    data = Convert.ToString(Console.ReadLine());
                    if (isOnlyInt(data) && Convert.ToInt32(data) > 0 && Convert.ToInt32(data) <= rowsCurrencies)
                    {
                        break;
                    }
                }

                if (column == 3)
                {
                    Console.WriteLine("Enter sale rate: ");
                    data = Convert.ToString(Console.ReadLine());
                    if (isOnlyIntAndPoint(data) && data.Length > 0 && data.Length < 10)
                    {
                        break;
                    }
                }

                if (column == 4)
                {
                    Console.WriteLine("Enter purchase rate: ");
                    data = Convert.ToString(Console.ReadLine());
                    if (isOnlyIntAndPoint(data) && data.Length > 0 && data.Length < 10)
                    {
                        break;
                    }
                }
                Console.WriteLine("Incorrect value!");
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var rates = HQLHelper.getObjectByN(session, "FROM Rates ORDER BY Id", Convert.ToInt32(id)).List();
                    foreach (Rates rate in rates)
                    {
                        switch (column)
                        {
                            case 1:
                                {
                                    var currenciesSold = HQLHelper.getObjectByN(session, "FROM Currencies ORDER BY Id", Convert.ToInt32(data)).List();
                                    foreach (Currencies currency in currenciesSold)
                                    {
                                        rate.IdCurrencySold = currency;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    var currenciesPurchased = HQLHelper.getObjectByN(session, "FROM Currencies ORDER BY Id", Convert.ToInt32(data)).List();
                                    foreach (Currencies currency in currenciesPurchased)
                                    {
                                        rate.IdCurrencyPurchased = currency;
                                    }
                                }
                                break;
                            case 3:
                                rate.SaleRate = double.Parse(data, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                            case 4:
                                rate.PurchaseRate = double.Parse(data, System.Globalization.CultureInfo.InvariantCulture);
                                break;
                        }
                        session.Update(rate);
                    }
                    transaction.Commit();
                }
            }
            Console.WriteLine("Node updated!");
        }

        public void searchRates()
        {

            string data;
            Console.WriteLine("Enter data: ");
            data = Convert.ToString(Console.ReadLine());

            using (var session = NHibernateHelper.OpenSession())
            {
                var rates = HQLHelper.searchRates(session, data).List();
                if(rates.Count > 0)
                {
                    Console.WriteLine("{0}{1}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                    Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}| {3, -20}| {4, -20}|", "N", "sold", "purchased", "sale_rate", "purchase_rate");
                    Console.WriteLine("{0}{1}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                    int i = 1;
                    foreach (Rates rate in rates)
                    {
                        Console.WriteLine("| {0, -3}| {1, -20}| {2, -20}| {3, -20}| {4, -20}|", i, rate.IdCurrencySold.Name, rate.IdCurrencyPurchased.Name, rate.SaleRate, rate.PurchaseRate);
                        Console.WriteLine("{0}{1}{1}{1}{1}+", "+".PadRight(5, '-'), "+".PadRight(22, '-'));
                        i++;
                    }
                } else
                {
                    Console.WriteLine("Nothing found!");
                }
            }
        }

        public void viewTransactions()
        {
            int pageTransactions = 1;
            int rows = HQLHelper.getTransactionsCountRows();
            if (rows > 0)
            {
                int id1 = 1 + (pageTransactions - 1) * countObjectOnPage;
                int id2 = countObjectOnPage + (pageTransactions - 1) * countObjectOnPage;
                using (var session = NHibernateHelper.OpenSession())
                {
                    bool exit = false;
                    while (!exit)
                    {
                        var transactions = HQLHelper.getListInRange(session, "FROM Transactions ORDER BY Id", id1, id2).List();
                        Console.WriteLine("{0}{1}{1}{1}{1}{2}{2}{1}{1}{2}{2}+", "+".PadRight(5, '-'), "+".PadRight(17, '-'), "+".PadRight(22, '-'));
                        Console.WriteLine("| {0, -3}| {1, -15}| {2, -15}| {3, -15}| {4, -15}| {5, -20}| {6, -20}| {7, -15}| {8, -15}| {9, -20}| {10, -20}|", "N", "sold", "purchased", "client", "cashier", "sale_rate", "purchase_rate", "date", "time", "sum_sold", "sum_purchased");
                        Console.WriteLine("{0}{1}{1}{1}{1}{2}{2}{1}{1}{2}{2}+", "+".PadRight(5, '-'), "+".PadRight(17, '-'), "+".PadRight(22, '-'));
                        int i = id1;
                        foreach (Transactions transaction in transactions)
                        {
                            Console.WriteLine("| {0, -3}| {1, -15}| {2, -15}| {3, -15}| {4, -15}| {5, -20}| {6, -20}| {7, -15}| {8, -15}| {9, -20}| {10, -20}|", i, transaction.IdCurrencySold.Name, transaction.IdCurrencyPurchased.Name, transaction.IdClient.Surname, transaction.IdCashier.Surname, transaction.IdRateSold.SaleRate, transaction.IdRatePurchased.PurchaseRate, transaction.Date, transaction.Time, transaction.SumCurrencySold, transaction.SumCurrencyPurchased);
                            Console.WriteLine("{0}{1}{1}{1}{1}{2}{2}{1}{1}{2}{2}+", "+".PadRight(5, '-'), "+".PadRight(17, '-'), "+".PadRight(22, '-'));
                            i++;
                        }

                        int maxPage = rows % countObjectOnPage == 0 ? rows / countObjectOnPage : rows / countObjectOnPage + 1;
                        Console.WriteLine("pages: " + pageTransactions + "/" + maxPage);
                        Console.WriteLine("1) next");
                        Console.WriteLine("2) prev");
                        Console.WriteLine("3) Exit");
                        Console.WriteLine("Enter action: ");
                        int action = Convert.ToInt32(Console.ReadLine());

                        switch (action)
                        {
                            case 1:
                                {
                                    if (pageTransactions < maxPage)
                                    {
                                        id1 += countObjectOnPage;
                                        id2 += countObjectOnPage;
                                        pageTransactions++;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if (pageTransactions > 1)
                                    {
                                        id1 -= countObjectOnPage;
                                        id2 -= countObjectOnPage;
                                        pageTransactions--;
                                    }
                                }
                                break;
                            case 3:
                                exit = true;
                                break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Object is empty!");
            }
        }

        public void insertTransactions()
        {
            int rows = HQLHelper.getTransactionsCountRows();
            int rowsCurrencies = HQLHelper.getCurrenciesCountRows();
            int rowsClients = HQLHelper.getClientsCountRows();
            int rowsCashiers = HQLHelper.getCashiersCountRows();
            int rowsRates = HQLHelper.getRatesCountRows();

            string N_currency_sold;
            while (true)
            {
                Console.WriteLine("Enter N currencies sold: ");
                N_currency_sold = Convert.ToString(Console.ReadLine());
                if (Convert.ToInt32(N_currency_sold) > 0 && Convert.ToInt32(N_currency_sold) <= rowsCurrencies && isOnlyInt(N_currency_sold))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string N_currency_purchased;
            while (true)
            {
                Console.WriteLine("Enter N currencies purchased: ");
                N_currency_purchased = Convert.ToString(Console.ReadLine());
                if (Convert.ToInt32(N_currency_purchased) != Convert.ToInt32(N_currency_sold) && Convert.ToInt32(N_currency_purchased) > 0 && Convert.ToInt32(N_currency_purchased) <= rowsCurrencies && isOnlyInt(N_currency_purchased))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string N_client;
            while (true)
            {
                Console.WriteLine("Enter N client: ");
                N_client = Convert.ToString(Console.ReadLine());
                if (Convert.ToInt32(N_client) > 0 && Convert.ToInt32(N_client) <= rowsClients && isOnlyInt(N_client))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string N_cashier;
            while (true)
            {
                Console.WriteLine("Enter N cashier: ");
                N_cashier = Convert.ToString(Console.ReadLine());
                if (Convert.ToInt32(N_cashier) > 0 && Convert.ToInt32(N_cashier) <= rowsCashiers && isOnlyInt(N_cashier))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string N_rate_sold;
            while (true)
            {
                Console.WriteLine("Enter N rate sold: ");
                N_rate_sold = Convert.ToString(Console.ReadLine());
                if (Convert.ToInt32(N_rate_sold) > 0 && Convert.ToInt32(N_rate_sold) <= rowsRates && isOnlyInt(N_rate_sold))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string N_rate_purchased;
            while (true)
            {
                Console.WriteLine("Enter N rate purchased: ");
                N_rate_purchased = Convert.ToString(Console.ReadLine());
                if (Convert.ToInt32(N_rate_purchased) > 0 && Convert.ToInt32(N_rate_purchased) <= rowsRates && isOnlyInt(N_rate_purchased))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            bool checkRatesSold = true;
            bool checkRatesPurchased = true;
            double rate_purchased = 0;
            double rate_sold = 0;
            using (var session = NHibernateHelper.OpenSession())
            {
                var currenciesSold = HQLHelper.getObjectByN(session, "FROM Currencies ORDER BY Id", Convert.ToInt32(N_currency_sold)).List();
                var currenciesPurchased = HQLHelper.getObjectByN(session, "FROM Currencies ORDER BY Id", Convert.ToInt32(N_currency_purchased)).List();
                var ratesSold = HQLHelper.getObjectByN(session, "FROM Rates ORDER BY Id", Convert.ToInt32(N_rate_sold)).List();
                var ratesPurchased = HQLHelper.getObjectByN(session, "FROM Rates ORDER BY Id", Convert.ToInt32(N_rate_purchased)).List();
                foreach (Rates rate in ratesSold)
                {
                    foreach (Currencies currency in currenciesSold)
                    {
                        if (rate.IdCurrencySold.Id != currency.Id)
                        {
                            checkRatesSold = false;
                        }
                    }
                    foreach (Currencies currency in currenciesPurchased)
                    {
                        if (rate.IdCurrencyPurchased.Id != currency.Id)
                        {
                            checkRatesSold = false;
                        }
                    }
                    rate_sold = rate.SaleRate;
                }
                foreach (Rates rate in ratesPurchased)
                {
                    foreach (Currencies currency in currenciesSold)
                    {
                        if (rate.IdCurrencySold.Id != currency.Id)
                        {
                            checkRatesPurchased = false;
                        }
                    }
                    foreach (Currencies currency in currenciesPurchased)
                    {
                        if (rate.IdCurrencyPurchased.Id != currency.Id)
                        {
                            checkRatesPurchased = false;
                        }
                    }
                    rate_purchased = rate.PurchaseRate;
                }
            }

            if(checkRatesSold && checkRatesPurchased)
            {
                int action;
                while (true)
                {
                    Console.WriteLine("1) buy currency");
                    Console.WriteLine("2) sell currency");
                    Console.WriteLine("Enter action:");
                    action = Convert.ToInt32(Console.ReadLine());
                    if (action >= 1 && action <= 2)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect value!");
                    }
                }

                string sum;
                while (true)
                {
                    Console.WriteLine("Enter the amount of currency to transfer: ");
                    sum = Convert.ToString(Console.ReadLine());
                    if (sum.Length > 0 && sum.Length < 10 && isOnlyIntAndPoint(sum))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect value!");
                    }
                }


                if (action == 1)
                {
                    rate_purchased = double.Parse(sum, System.Globalization.CultureInfo.InvariantCulture) * rate_sold;
                    rate_sold = double.Parse(sum, System.Globalization.CultureInfo.InvariantCulture);
                }

                if (action == 2)
                {
                    rate_sold = double.Parse(sum, System.Globalization.CultureInfo.InvariantCulture) / rate_purchased;
                    rate_purchased = double.Parse(sum, System.Globalization.CultureInfo.InvariantCulture);
                }

                using (var session = NHibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        Transactions newTransactions = new Transactions();
                        var currenciesSold = HQLHelper.getObjectByN(session, "FROM Currencies ORDER BY Id", Convert.ToInt32(N_currency_sold)).List();
                        foreach (Currencies currency in currenciesSold)
                        {
                            newTransactions.IdCurrencySold = currency;
                        }
                        var currenciesPurchased = HQLHelper.getObjectByN(session, "FROM Currencies ORDER BY Id", Convert.ToInt32(N_currency_purchased)).List();
                        foreach (Currencies currency in currenciesPurchased)
                        {
                            newTransactions.IdCurrencyPurchased = currency;
                        }
                        var clients = HQLHelper.getObjectByN(session, "FROM Clients ORDER BY Id", Convert.ToInt32(N_client)).List();
                        foreach (Clients client in clients)
                        {
                            newTransactions.IdClient = client;
                        }
                        var cashiers = HQLHelper.getObjectByN(session, "FROM Cashiers ORDER BY Id", Convert.ToInt32(N_cashier)).List();
                        foreach (Cashiers cashier in cashiers)
                        {
                            newTransactions.IdCashier = cashier;
                        }
                        var ratesSold = HQLHelper.getObjectByN(session, "FROM Rates ORDER BY Id", Convert.ToInt32(N_rate_sold)).List();
                        foreach (Rates rate in ratesSold)
                        {
                            newTransactions.IdRateSold = rate;
                        }
                        var ratesPurchased = HQLHelper.getObjectByN(session, "FROM Rates ORDER BY Id", Convert.ToInt32(N_rate_purchased)).List();
                        foreach (Rates rate in ratesPurchased)
                        {
                            newTransactions.IdRatePurchased = rate;
                        }
                        newTransactions.Date = getDate();
                        newTransactions.Time = getTime();
                        newTransactions.SumCurrencySold = rate_sold;
                        newTransactions.SumCurrencyPurchased = rate_purchased;
                        session.Save(newTransactions);
                        transaction.Commit();
                    }
                }
                Console.WriteLine("Node inserted!");
            } else
            {
                Console.WriteLine("Error! The exchange rate does not match the currency");
            }
        }

        public void deleteTransactions()
        {
            int rows = HQLHelper.getTransactionsCountRows();
            string id;
            while (true)
            {
                Console.WriteLine("Enter N nodes: ");
                id = Convert.ToString(Console.ReadLine());
                if (isOnlyInt(id) && Convert.ToInt32(id) > 0 && Convert.ToInt32(id) <= rows)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var transactions = HQLHelper.getObjectByN(session, "FROM Transactions ORDER BY Id", Convert.ToInt32(id)).List();
                    foreach (Transactions trans in transactions)
                    {
                        session.Delete(trans);
                    }
                    transaction.Commit();
                }
            }
            Console.WriteLine("Node deleted!");
        }

        public void updateTransactions()
        {
            string id;
            int rows = HQLHelper.getTransactionsCountRows();
            int rowsCurrencies = HQLHelper.getCurrenciesCountRows();
            int rowsClients = HQLHelper.getClientsCountRows();
            int rowsCashiers = HQLHelper.getCashiersCountRows();
            int rowsRates = HQLHelper.getRatesCountRows();

            while (true)
            {
                Console.WriteLine("Enter N nodes: ");
                id = Convert.ToString(Console.ReadLine());
                if (isOnlyInt(id) && Convert.ToInt32(id) > 0 && Convert.ToInt32(id) <= rows)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            int column;
            while (true)
            {
                Console.WriteLine("1)sold");
                Console.WriteLine("2)purchased");
                Console.WriteLine("3)client");
                Console.WriteLine("4)cashier");
                Console.WriteLine("5)rate_sold");
                Console.WriteLine("6)rate_purchased");
                Console.WriteLine("7)date");
                Console.WriteLine("8)time");
                Console.WriteLine("9)sum_sold");
                Console.WriteLine("10)sum_purchased");
                Console.WriteLine("Enter column: ");
                column = Convert.ToInt32(Console.ReadLine());
                if (column >= 1 && column <= 10)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }

            string data;
            bool checkRatesSold = true;
            bool checkRatesPurchased = true;
            while (true)
            {

                if (column == 1)
                {
                    Console.WriteLine("Enter N currencies sold: ");
                    data = Convert.ToString(Console.ReadLine());
                    if (isOnlyInt(data) && Convert.ToInt32(data) > 0 && Convert.ToInt32(data) <= rowsCurrencies)
                    {
                        break;
                    }
                }

                if (column == 2)
                {
                    Console.WriteLine("Enter N currencies purchased: ");
                    data = Convert.ToString(Console.ReadLine());
                    if (isOnlyInt(data) && Convert.ToInt32(data) > 0 && Convert.ToInt32(data) <= rowsCurrencies)
                    {
                        break;
                    }
                }

                if (column == 3)
                {
                    Console.WriteLine("Enter N client: ");
                    data = Convert.ToString(Console.ReadLine());
                    if (isOnlyInt(data) && Convert.ToInt32(data) > 0 && Convert.ToInt32(data) <= rowsClients)
                    {
                        break;
                    }
                }

                if (column == 4)
                {
                    Console.WriteLine("Enter N cashier: ");
                    data = Convert.ToString(Console.ReadLine());
                    if (isOnlyInt(data) && Convert.ToInt32(data) > 0 && Convert.ToInt32(data) <= rowsCashiers)
                    {
                        break;
                    }
                }

                if (column == 5)
                {
                    Console.WriteLine("Enter N rate sold: ");
                    data = Convert.ToString(Console.ReadLine());
                    if (isOnlyInt(data) && Convert.ToInt32(data) > 0 && Convert.ToInt32(data) <= rowsRates)
                    {
                        using (var session = NHibernateHelper.OpenSession())
                        {
                            var transactions = HQLHelper.getObjectByN(session, "FROM Transactions ORDER BY Id", Convert.ToInt32(id)).List();
                            var ratesSold = HQLHelper.getObjectByN(session, "FROM Rates ORDER BY Id", Convert.ToInt32(data)).List();
                            foreach (Transactions trans in transactions)
                            {
                                foreach (Rates rate in ratesSold)
                                {
                                    if (trans.IdCurrencySold.Id != rate.IdCurrencySold.Id)
                                    {
                                        checkRatesSold = false;
                                    }
                                }
                            }
   
                        }
                        break;
                    }
                }

                if (column == 6)
                {
                    Console.WriteLine("Enter N rate purchased: ");
                    data = Convert.ToString(Console.ReadLine());
                    if (isOnlyInt(data) && Convert.ToInt32(data) > 0 && Convert.ToInt32(data) <= rowsRates)
                    {
                        using (var session = NHibernateHelper.OpenSession())
                        {
                            var transactions = HQLHelper.getObjectByN(session, "FROM Transactions ORDER BY Id", Convert.ToInt32(id)).List();
                            var ratesPurchased = HQLHelper.getObjectByN(session, "FROM Rates ORDER BY Id", Convert.ToInt32(data)).List();
                            foreach (Transactions trans in transactions)
                            {
                                foreach (Rates rate in ratesPurchased)
                                {
                                    if (trans.IdCurrencyPurchased.Id != rate.IdCurrencyPurchased.Id)
                                    {
                                        checkRatesPurchased = false;
                                    }
                                }
                            }

                        }
                        break;
                    }
                }

                if (column == 7)
                {
                    Console.WriteLine("Enter date: ");
                    data = Convert.ToString(Console.ReadLine());
                    if (isDate(data))
                    {
                        break;
                    }
                }

                if (column == 8)
                {
                    Console.WriteLine("Enter time: ");
                    data = Convert.ToString(Console.ReadLine());
                    if (isTime(data))
                    {
                        break;
                    }
                }

                if (column == 9)
                {
                    Console.WriteLine("Enter sum sold: ");
                    data = Convert.ToString(Console.ReadLine());
                    if (isOnlyIntAndPoint(data) && data.Length > 0 && data.Length < 10)
                    {
                        break;
                    }
                }

                if (column == 10)
                {
                    Console.WriteLine("Enter sum purchased: ");
                    data = Convert.ToString(Console.ReadLine());
                    if (isOnlyIntAndPoint(data) && data.Length > 0 && data.Length < 10)
                    {
                        break;
                    }
                }

                Console.WriteLine("Incorrect value!");
            }

            if(checkRatesSold && checkRatesPurchased)
            {
                using (var session = NHibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var transactions = HQLHelper.getObjectByN(session, "FROM Transactions ORDER BY Id", Convert.ToInt32(id)).List();
                        foreach (Transactions trans in transactions)
                        {
                            switch (column)
                            {
                                case 1:
                                    {
                                        var currenciesSold = HQLHelper.getObjectByN(session, "FROM Currencies ORDER BY Id", Convert.ToInt32(data)).List();
                                        foreach (Currencies currency in currenciesSold)
                                        {
                                            trans.IdCurrencySold = currency;
                                        }
                                    }
                                    break;
                                case 2:
                                    {
                                        var currenciesPurchased = HQLHelper.getObjectByN(session, "FROM Currencies ORDER BY Id", Convert.ToInt32(data)).List();
                                        foreach (Currencies currency in currenciesPurchased)
                                        {
                                            trans.IdCurrencyPurchased = currency;
                                        }
                                    }
                                    break;
                                case 3:
                                    {
                                        var clients = HQLHelper.getObjectByN(session, "FROM Clients ORDER BY Id", Convert.ToInt32(data)).List();
                                        foreach (Clients client in clients)
                                        {
                                            trans.IdClient = client;
                                        }
                                    }
                                    break;
                                case 4:
                                    {
                                        var cashiers = HQLHelper.getObjectByN(session, "FROM Cashiers ORDER BY Id", Convert.ToInt32(data)).List();
                                        foreach (Cashiers cashier in cashiers)
                                        {
                                            trans.IdCashier = cashier;
                                        }
                                    }
                                    break;
                                case 5:
                                    {
                                        var ratesSold = HQLHelper.getObjectByN(session, "FROM Rates ORDER BY Id", Convert.ToInt32(data)).List();
                                        foreach (Rates rate in ratesSold)
                                        {
                                            trans.IdRateSold = rate;
                                        }
                                    }
                                    break;
                                case 6:
                                    {
                                        var ratesPurchased = HQLHelper.getObjectByN(session, "FROM Rates ORDER BY Id", Convert.ToInt32(data)).List();
                                        foreach (Rates rate in ratesPurchased)
                                        {
                                            trans.IdRatePurchased = rate;
                                        }
                                    }
                                    break;
                                case 7:
                                    trans.Date = data;
                                    break;
                                case 8:
                                    trans.Time = data;
                                    break;
                                case 9:
                                    trans.SumCurrencySold = double.Parse(data, System.Globalization.CultureInfo.InvariantCulture);
                                    break;
                                case 10:
                                    trans.SumCurrencyPurchased = double.Parse(data, System.Globalization.CultureInfo.InvariantCulture);
                                    break;

                            }
                            session.Update(trans);
                        }
                        transaction.Commit();
                    }
                }
                Console.WriteLine("Node updated!");
            } else
            {
                Console.WriteLine("Error! The exchange rate does not match the currency");
            }
        }

        public void searchTransactions()
        {

            string data;
            Console.WriteLine("Enter data: ");
            data = Convert.ToString(Console.ReadLine());

            using (var session = NHibernateHelper.OpenSession())
            {
                var transactions = HQLHelper.searchTransactions(session, data).List();
                if (transactions.Count > 0)
                {
                    Console.WriteLine("{0}{1}{1}{1}{1}{2}{2}{1}{1}{2}{2}+", "+".PadRight(5, '-'), "+".PadRight(17, '-'), "+".PadRight(22, '-'));
                    Console.WriteLine("| {0, -3}| {1, -15}| {2, -15}| {3, -15}| {4, -15}| {5, -20}| {6, -20}| {7, -15}| {8, -15}| {9, -20}| {10, -20}|", "N", "sold", "purchased", "client", "cashier", "sale_rate", "purchase_rate", "date", "time", "sum_sold", "sum_purchased");
                    Console.WriteLine("{0}{1}{1}{1}{1}{2}{2}{1}{1}{2}{2}+", "+".PadRight(5, '-'), "+".PadRight(17, '-'), "+".PadRight(22, '-'));
                    int i = 1;
                    foreach (Transactions transaction in transactions)
                    {
                        Console.WriteLine("| {0, -3}| {1, -15}| {2, -15}| {3, -15}| {4, -15}| {5, -20}| {6, -20}| {7, -15}| {8, -15}| {9, -20}| {10, -20}|", i, transaction.IdCurrencySold.Name, transaction.IdCurrencyPurchased.Name, transaction.IdClient.Surname, transaction.IdCashier.Surname, transaction.IdRateSold.SaleRate, transaction.IdRatePurchased.PurchaseRate, transaction.Date, transaction.Time, transaction.SumCurrencySold, transaction.SumCurrencyPurchased);
                        Console.WriteLine("{0}{1}{1}{1}{1}{2}{2}{1}{1}{2}{2}+", "+".PadRight(5, '-'), "+".PadRight(17, '-'), "+".PadRight(22, '-'));
                        i++;
                    }
                }
                else
                {
                    Console.WriteLine("Nothing found!");
                }
            }
        }

        private bool isOnlyInt(string str)
        {
            bool result = true;
            for (int i = 0; i < str.Length; i++)
            {
                if (!((int)str[i] >= 48 && (int)str[i] <= 57))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private bool isOnlyIntAndPoint(string str)
        {
            bool result = true;
            for (int i = 0; i < str.Length; i++)
            {
                if (!(((int)str[i] >= 48 && (int)str[i] <= 57) || (int)str[i] == 46))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private bool isOnlyString(string str)
        {
            bool result = true;
            for (int i = 0; i < str.Length; i++)
            {
                if (!((int)str[i] >= 65 && (int)str[i] <= 122))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private string getDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        private string getTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        private bool isDate(string date)
        {
            bool result = true;
            if (date.Length != 10)
            {
                result = false;
            }
            for (int i = 0; i < date.Length; i++)
            {
                if (!(((int)date[i] >= 48 && (int)date[i] <= 57) || (int)date[i] == 45))
                {
                    result = false;
                    break;
                }
            }
            if ((int)date[4] != 45 || (int)date[7] != 45)
            {
                result = false;
            }
            if ((int)date[5] >= 48 || (int)date[5] <= 49)
            {
                result = false;
            }
            if ((int)date[8] >= 48 || (int)date[8] <= 51)
            {
                result = false;
            }
            return result;
        }

        private bool isTime(string time)
        {
            bool result = true;
            if (time.Length != 8)
            {
                result = false;
            }
            for (int i = 0; i < time.Length; i++)
            {
                if (!(((int)time[i] >= 48 && (int)time[i] <= 57) || (int)time[i] == 58))
                {
                    result = false;
                    break;
                }
            }
            if ((int)time[2] != 58 || (int)time[5] != 58)
            {
                result = false;
            }
            if ((int)time[0] >= 48 || (int)time[0] <= 50)
            {
                result = false;
            }
            if ((int)time[3] >= 48 || (int)time[3] <= 54)
            {
                result = false;
            }
            if ((int)time[6] >= 48 || (int)time[6] <= 54)
            {
                result = false;
            }
            return result;
        }
    }
}
