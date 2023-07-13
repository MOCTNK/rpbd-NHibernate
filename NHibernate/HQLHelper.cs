using lab_2.NHibernate.Data;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab_2.NHibernate
{
    public class HQLHelper
    {

        public static IQuery getListInRange(ISession session, string hql, int id1, int id2)
        {
            return session.CreateQuery(hql).SetFirstResult(id1 - 1).SetMaxResults(id2 - id1 + 1);
        }
        public static IQuery getObjectByN(ISession session, string hql, int id)
        {
            return getListInRange(session, hql, id, id);
        }

        public static int getClientsCountRows()
        {
            int rows;
            using (var session = NHibernateHelper.OpenSession())
            {
                rows = session.QueryOver<Clients>().Select(Projections.RowCount()).SingleOrDefault<int>();
            }
            return rows;
        }

        public static IQuery searchClients(ISession session, string data)
        {
            string hql = $"FROM Clients AS e WHERE e.Name LIKE '%{data}%' "
                         +$"OR e.Surname LIKE '%{data}%'"
                         +$"OR e.Patronymic LIKE '%{data}%'"
                         +$"OR e.Passport_series LIKE '%{data}%'"
                         +$"OR e.Passport_number LIKE '%{data}%'";
            return session.CreateQuery(hql);
        }

        public static int getCashiersCountRows()
        {
            int rows;
            using (var session = NHibernateHelper.OpenSession())
            {
                rows = session.QueryOver<Cashiers>().Select(Projections.RowCount()).SingleOrDefault<int>();
            }
            return rows;
        }

        public static IQuery searchCashiers(ISession session, string data)
        {
            string hql = $"FROM Cashiers AS e WHERE e.Name LIKE '%{data}%' "
                         + $"OR e.Surname LIKE '%{data}%'"
                         + $"OR e.Patronymic LIKE '%{data}%'";
            return session.CreateQuery(hql);
        }

        public static int getCurrenciesCountRows()
        {
            int rows;
            using (var session = NHibernateHelper.OpenSession())
            {
                rows = session.QueryOver<Currencies>().Select(Projections.RowCount()).SingleOrDefault<int>();
            }
            return rows;
        }

        public static IQuery searchCurrencies(ISession session, string data)
        {
            string hql = $"FROM Currencies AS e WHERE e.Code LIKE '%{data}%' "
                         + $"OR e.Name LIKE '%{data}%'";

            return session.CreateQuery(hql);
        }

        public static int getRatesCountRows()
        {
            int rows;
            using (var session = NHibernateHelper.OpenSession())
            {
                rows = session.QueryOver<Rates>().Select(Projections.RowCount()).SingleOrDefault<int>();
            }
            return rows;
        }

        public static IQuery searchRates(ISession session, string data)
        {

            string hql = $"FROM Rates AS e WHERE "
                         + $"e.IdCurrencySold.Name LIKE '%{data}%'"
                         + $" OR e.IdCurrencyPurchased.Name LIKE '%{data}%'"
                         + $" OR CAST(e.SaleRate AS string) LIKE '%{data}%'"
                         + $" OR CAST(e.PurchaseRate AS string) LIKE '%{data}%'";
            return session.CreateQuery(hql);
        }

        public static int getTransactionsCountRows()
        {
            int rows;
            using (var session = NHibernateHelper.OpenSession())
            {
                rows = session.QueryOver<Transactions>().Select(Projections.RowCount()).SingleOrDefault<int>();
            }
            return rows;
        }

        public static IQuery searchTransactions(ISession session, string data)
        {

            string hql = $"FROM Transactions AS e WHERE "
                         + $"e.IdCurrencySold.Name LIKE '%{data}%'"
                         + $" OR e.IdCurrencyPurchased.Name LIKE '%{data}%'"
                         + $" OR e.IdClient.Surname LIKE '%{data}%'"
                         + $" OR e.IdCashier.Surname LIKE '%{data}%'"
                         + $" OR CAST(e.IdRateSold.SaleRate AS string) LIKE '%{data}%'"
                         + $" OR CAST(e.IdRatePurchased.PurchaseRate AS string) LIKE '%{data}%'"
                         + $" OR e.Date LIKE '%{data}%'"
                         + $" OR e.Time LIKE '%{data}%'"
                         + $" OR CAST(e.SumCurrencySold AS string) LIKE '%{data}%'"
                         + $" OR CAST(e.SumCurrencyPurchased AS string) LIKE '%{data}%'";
            return session.CreateQuery(hql);
        }
    }
}

